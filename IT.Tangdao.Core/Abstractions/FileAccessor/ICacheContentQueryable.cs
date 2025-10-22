using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IT.Tangdao.Core.Enums;

namespace IT.Tangdao.Core.Abstractions.FileAccessor
{
    public interface ICacheContentQueryable : IContentQueryable
    {
        /// <summary>
        /// 唯一一个原子方法：拿带缓存的内容
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        new IContentQueryable Read(string path, DaoFileType type = DaoFileType.None);

        /// <summary>
        /// 清除一条缓存
        /// </summary>
        /// <param name="path"></param>
        void Clear(string path);

        /// <summary>
        /// 清除整个区域（可选）
        /// </summary>
        /// <param name="region"></param>
        void ClearRegion(string region);
    }
}