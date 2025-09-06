using IT.Tangdao.Core.DaoAdmin.Results;
using IT.Tangdao.Core.DaoDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.IServices
{
    public interface IFileReadService
    {
        /// <summary>
        /// 读取本地文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task<ReadResult> ReadLocalFile(string filePath);

        /// <summary>
        /// 读取网络文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task<ReadResult> ReadNetFile(string filePath);
    }
}