using IT.Tangdao.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoDtos.Items
{
    /// <summary>
    /// 用于简单的标题书写
    /// </summary>
    public class TangdaoMenuItem : IMenuItem
    {
        /// <summary>
        /// 配置Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        public IList<IMenuItem> Childs { get; set; }

        /// <summary>
        /// 获取或设置配置值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual string this[string key]
        {
            get
            {
                return this.Find(key)?.Value;
            }

            set
            {
                IMenuItem item = this.Find(key, true);
                if (item != null)
                {
                    item.Value = value;
                }
            }
        }
    }
}