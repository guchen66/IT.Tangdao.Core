using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Helpers
{
    public static class CultureHelper
    {
        /// <summary>
        /// 获取用户当前的文化设置
        /// </summary>
        public static CultureInfo GetUserCulture()
        {
            return Thread.CurrentThread.CurrentCulture ??
                   CultureInfo.DefaultThreadCurrentCulture ??
                   CultureInfo.CurrentCulture ??
                   CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// 获取用户当前的UI文化设置
        /// </summary>
        public static CultureInfo GetUserUICulture()
        {
            return Thread.CurrentThread.CurrentUICulture ??
                   CultureInfo.DefaultThreadCurrentUICulture ??
                   CultureInfo.CurrentUICulture ??
                   CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// 判断用户是否使用中文环境
        /// </summary>
        public static bool IsChineseCulture()
        {
            var culture = GetUserCulture();
            return culture.Name.StartsWith("zh-", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 获取基于用户文化的测试数据
        /// </summary>
        public static string[] GetCultureSpecificNames()
        {
            var culture = GetUserCulture();

            return culture.Name switch
            {
                string name when name.StartsWith("zh-") => new[] { "张三", "李四", "王五", "赵六", "钱七" },
                string name when name.StartsWith("ja-") => new[] { "佐藤", "鈴木", "高橋", "田中", "伊藤" },
                string name when name.StartsWith("ko-") => new[] { "김", "이", "박", "최", "정" },
                _ => new[] { "James", "Mary", "John", "Patricia", "Robert" } // 默认英文
            };
        }
    }
}