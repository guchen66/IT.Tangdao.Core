using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Abstractions.Results;
using System.Threading.Tasks;
using IT.Tangdao.Core.Helpers;
using IT.Tangdao.Core.Extensions;
using System.Xml.Linq;
using IT.Tangdao.Core.Paths;
using System.Windows.Controls;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public sealed class FileLocator : IFileLocator
    {
        /// <inheritdoc/>
        public IEnumerable<string> FindFiles(string directoryPath, string searchPattern, bool searchSubdirectories)
        {
            return InternalFindFiles(directoryPath, searchPattern, searchSubdirectories);
        }

        /// <inheritdoc/>
        public IEnumerable<string> FindFiles(AbsolutePath absolutePath, string searchPattern, bool searchSubdirectories)
        {
            return InternalFindFiles(absolutePath.Value, searchPattern, searchSubdirectories);
        }

        /// <inheritdoc/>
        public string FindFirst(string directoryPath, string searchPattern, bool searchSubdirectories)
        {
            return FindFiles(directoryPath, searchPattern, searchSubdirectories).FirstOrDefault();
        }

        /// <inheritdoc/>
        public string FindFirst(AbsolutePath absolutePath, string searchPattern, bool searchSubdirectories)
        {
            return FindFiles(absolutePath.Value, searchPattern, searchSubdirectories).FirstOrDefault();
        }

        /// <summary>
        /// 共享核心逻辑
        /// </summary>
        private static IEnumerable<string> InternalFindFiles(string directory, string? searchPattern, bool searchSubdirectories)
        {
            if (!Directory.Exists(directory))
                return Enumerable.Empty<string>();

            var option = new EnumerationOptions
            {
                RecurseSubdirectories = searchSubdirectories,
                IgnoreInaccessible = true
            };
            var patterns = string.IsNullOrWhiteSpace(searchPattern)
                ? new[] { "*.*" }
                : searchPattern
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.StartsWith('*') ? p : $"*{p.Trim()}")
                    .ToArray();

            return patterns.SelectMany(p => Directory.EnumerateFiles(directory, p, option))
                           .Distinct(StringComparer.OrdinalIgnoreCase);
        }

        /// <inheritdoc/>
        public async Task<TEntity> ReadXmlToEntityAsync<TEntity>(string path, DaoFileType daoFileType) where TEntity : class, new()
        {
            string xml = string.Empty;
            TEntity Entity = new TEntity();
            if (daoFileType == DaoFileType.Xml)
            {
                xml = await ReadXmlAsync(path);
                Entity = TangdaoXmlSerializer.Deserialize<TEntity>(xml);
            }
            return Entity;
        }

        public ResponseResult<string> BatchReadFileAsync(string path, DaoFileType daoFileType = DaoFileType.Txt)
        {
            if (string.IsNullOrWhiteSpace(path))
                return ResponseResult<string>.Failure("路径不能为空。");

            if (FileHelper.GetPathKind(path) != PathKind.Directory)
                return ResponseResult<string>.Failure("指定路径必须是有效目录。");

            // 1. 先拿过滤后的文件列表（私有方法，见下）
            var files = QueryFilter(path, daoFileType);
            if (!files.Any())
                return ResponseResult<string>.Failure($"目录下未找到 {daoFileType} 类型文件。");

            // 2. 逐个读并合并
            var sb = new StringBuilder();
            foreach (var file in files)
            {
                try
                {
                    sb.AppendLine(File.ReadAllText(file));
                }
                catch (Exception ex)
                {
                    return ResponseResult<string>.Failure($"读取文件失败：{ex.Message}");
                }
            }
            return ResponseResult<string>.Success(sb.ToString());
        }

        /// <summary>
        /// 私有过滤：只返回指定后缀的文件路径。
        /// </summary>
        private static IEnumerable<string> QueryFilter(string directoryPath, DaoFileType type)
        {
            switch (type)
            {
                case DaoFileType.None:
                    break;

                case DaoFileType.Txt:
                    break;

                case DaoFileType.Xml:
                    break;

                case DaoFileType.Xlsx:
                    break;

                case DaoFileType.Xaml:
                    break;

                case DaoFileType.Json:
                    break;

                case DaoFileType.Config:
                    break;

                default:
                    break;
            }

            return Directory.EnumerateFiles(directoryPath, $"*{type}", SearchOption.AllDirectories);
        }

        private static async Task<string> ReadTxtAsync(string path)
        {
            using (var stream = new StreamReader(path.UseFileOpenRead()))
            {
                return await stream.ReadToEndAsync();
            }
        }

        private static async Task<string> ReadXmlAsync(string path)
        {
            using (var stream = new StreamReader(path.UseFileOpenRead()))
            {
                var xmlContent = await stream.ReadToEndAsync();
                // 这里可以根据需要对xmlContent进行解析
                // 例如，使用XDocument加载XML内容
                var doc = XDocument.Parse(xmlContent);
                // 对doc进行处理，例如提取数据
                // ...
                // 返回处理后的字符串或XML文档的字符串表示
                return doc.ToString();
            }
        }
    }
}