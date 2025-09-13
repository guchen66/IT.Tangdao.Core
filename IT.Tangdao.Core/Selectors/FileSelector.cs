using IT.Tangdao.Core.Abstractions;
using IT.Tangdao.Core.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IT.Tangdao.Core.Selectors
{
    internal sealed class FileSelector
    {
        private static Lazy<IRead> _read = new Lazy<IRead>(() => new Read());

        public static IRead Queryable()
        {
            return _read.Value;
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

            if (trimmedContent.StartsWith("{") && trimmedContent.EndsWith("}") ||
                trimmedContent.StartsWith("[") && trimmedContent.EndsWith("]"))
            {
                return DaoFileType.Json;
            }
            else if (trimmedContent.StartsWith("<") && trimmedContent.EndsWith(">"))
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

        public static void MapXElementToObject<T>(XElement node, T instance)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                var element = node.Element(prop.Name); // 自动匹配同名节点
                if (element == null) continue;

                try
                {
                    object value = Convert.ChangeType(element.Value, prop.PropertyType);
                    prop.SetValue(instance, value);
                }
                catch
                {
                    // 类型转换失败时跳过（或记录日志）
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