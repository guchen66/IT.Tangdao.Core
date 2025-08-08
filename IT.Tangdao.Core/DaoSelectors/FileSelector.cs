using IT.Tangdao.Core.DaoAdmin;
using IT.Tangdao.Core.DaoEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IT.Tangdao.Core.DaoSelectors
{
    public class FileSelector
    {
        private static Lazy<IRead> _read = new Lazy<IRead>(() => new Read());

        public static IRead Queryable()
        {
            return _read.Value;
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