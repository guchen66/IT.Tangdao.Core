using IT.Tangdao.Core.Abstractions;
using IT.Tangdao.Core.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IT.Tangdao.Core.Selectors
{
    internal sealed class FileSelector
    {
        private FileSelector()
        {
        }

        /// <summary>
        /// 获取当前路径文件类型
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static PathKind GetPathKind(string path)
        {
            if (!Directory.Exists(path) && !File.Exists(path))
                return PathKind.None;

            // 目录存在就优先认目录
            return (File.GetAttributes(path) & FileAttributes.Directory) != 0
                ? PathKind.Directory
                : PathKind.File;
        }

        /// <summary>
        /// 解析当前类型属于指定枚举
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static DaoFileType DetectFromContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return DaoFileType.None;
            }

            string trimmedContent = content.Trim();

            if (trimmedContent.StartsWith('{') && trimmedContent.EndsWith('}') ||
                trimmedContent.StartsWith('[') && trimmedContent.EndsWith(']'))
            {
                return DaoFileType.Json;
            }
            else if (trimmedContent.StartsWith('<') && trimmedContent.EndsWith('>'))
            {
                return DaoFileType.Xml;
            }
            // 可以添加更多文件类型的检测逻辑
            else
            {
                // 如果都不匹配，可以尝试更复杂的检测或返回None
                return DaoFileType.None;
            }
        }

        public static DaoXmlType DetectXmlStructure(XDocument doc)
        {
            if (doc == null) return DaoXmlType.Empty;

            var root = doc.Root;

            // 检查是否只有XML声明没有内容
            if (root == null) return DaoXmlType.None;

            // 检查根节点是否有子元素
            if (!root.HasElements) return DaoXmlType.Empty;

            // 获取根节点的直接子元素
            var elements = root.Elements();

            // 只有一个子元素的情况
            if (elements.Count() == 1)
            {
                return DaoXmlType.Single;
            }

            // 多个子元素的情况
            return DaoXmlType.Multiple;
        }

        /// <summary>
        /// 把 XElement 节点的**同名子元素**映射到已有对象的可写属性上。
        /// 只映射 public 实例属性，且节点名必须与属性名完全一致（大小写敏感）。
        /// 类型转换失败时静默跳过，不会抛异常。
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="node">XML 节点</param>
        /// <param name="instance">**已创建**的实例，字段会被填充</param>
        public static void MapXElementToObject<T>(XElement node, T instance)
        {
            // 1. 取出所有公共可写属性
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                // 2. 按属性名去找同名子元素
                var element = node.Element(prop.Name);
                if (element == null) continue;          // 没有对应节点就跳过

                try
                {
                    // 3. 把字符串值转成属性类型，再反射赋值
                    object value = Convert.ChangeType(element.Value, prop.PropertyType);
                    prop.SetValue(instance, value);
                }
                catch
                {
                    // 4. 转换失败（如格式不对、只读属性等）直接忽略
                }
            }
        }

        /// <summary>
        /// 搜索指定目录的所有指定后缀的文件
        /// </summary>
        /// <param name="rootDir"></param>
        /// <param name="fileType"></param>
        /// <param name="searchSubdirectories"></param>
        /// <returns></returns>
        public static IEnumerable<string> SelectFilesByDaoFileType(string rootDir, DaoFileType fileType, bool searchSubdirectories = true)
        {
            if (fileType == DaoFileType.None)
                return Enumerable.Empty<string>();

            string extension = GetExtensionFromFileType(fileType);
            if (string.IsNullOrEmpty(extension))
                return Enumerable.Empty<string>();

            var searchOption = searchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            try
            {
                return Directory.EnumerateFiles(rootDir, $"*{extension}", searchOption);
            }
            catch (UnauthorizedAccessException)
            {
                return Enumerable.Empty<string>();
            }
        }

        public static string GetExtensionFromFileType(DaoFileType fileType)
        {
            return fileType switch
            {
                DaoFileType.Txt => ".txt",
                DaoFileType.Xml => ".xml",
                DaoFileType.Xlsx => ".xlsx",
                DaoFileType.Xaml => ".xaml",
                DaoFileType.Json => ".json",
                DaoFileType.Config => ".config",
                _ => null
            };
        }

        public static ChannelReader<string> EnumerateFilesRecursively(string root, int capacity = 100, CancellationToken token = default)
        {
            var output = Channel.CreateBounded<string>(capacity);             //创建一个有界通道

            async Task WalkDir(string path)
            {
                IEnumerable<string> files = null, directories = null;           //List列表
                try
                {
                    files = Directory.EnumerateFiles(path);                    //搜索匹配的目录，子目录，文件，子文件
                    directories = Directory.EnumerateDirectories(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                if (files != null)
                {
                    foreach (var file in files)
                    {
                        await output.Writer.WriteAsync(file, token);                //将文件名写入channel
                    }
                }

                if (directories != null)
                    await Task.WhenAll(directories.Select(WalkDir));
            }

            Task.Run(async () =>
            {
                await WalkDir(root);
                output.Writer.Complete();                                           //停止写入
            }, token);

            return output.Reader;                                                     //读取
        }

        public static ChannelReader<FileInfo> FilterByExtension(ChannelReader<string> input, IReadOnlySet<string> exts, CancellationToken token = default)
        {
            var output = Channel.CreateUnbounded<FileInfo>();               //无界通道

            Task.Run(async () =>
            {
                try
                {
                    await foreach (var file in input.ReadAllAsync(token).ConfigureAwait(false)) //筛选条件，读取，不需要将后续代码切到UI线程
                    {
                        var fileInfo = new FileInfo(file);
                        if (exts.Contains(fileInfo.Extension))
                            await output.Writer.WriteAsync(fileInfo, token).ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                finally
                {
                    output.Writer.Complete();
                }
            }, token);

            return output;
        }

        public static async Task ReadDataFromFile(string filePath, ChannelWriter<string> writer, CancellationToken token = default)
        {
            try
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = await streamReader.ReadLineAsync()) != null)
                    {
                        await writer.WriteAsync(line, token);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading data from file: {ex.Message}");
            }
            finally
            {
                writer.Complete();
            }
        }

        public static async Task WriteDataToFile(string filePath, ChannelReader<string> reader, CancellationToken token = default)
        {
            try
            {
                using (var streamWriter = new StreamWriter(filePath))
                {
                    await foreach (var data in reader.ReadAllAsync(token).ConfigureAwait(false))
                    {
                        await streamWriter.WriteLineAsync(data);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing data to file: {ex.Message}");
            }
        }

        /// <summary>
        /// 文件导入
        /// </summary>
        public static void Import()
        {
        }

        /// <summary>
        /// 文件导出
        /// </summary>
        public static void Export()
        {
        }
    }
}