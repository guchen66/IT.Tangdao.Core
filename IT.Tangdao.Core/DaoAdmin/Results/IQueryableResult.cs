using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.Results
{
    /// <summary>
    /// 基础结果接口
    /// </summary>
    public interface IQueryableResult
    {
        bool IsSuccess { get; }
        string Message { get; }
        DateTime Timestamp { get; }
    }
}