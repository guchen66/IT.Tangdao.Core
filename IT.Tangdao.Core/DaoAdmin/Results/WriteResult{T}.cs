using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.Results
{
    /// <summary>
    /// 泛型写入结果
    /// </summary>
    /// <typeparam name="T">返回的数据类型</typeparam>
    public class WriteResult<T> : WriteResult, IQueryableResult<T>
    {
        public T Data { get; private set; }

        private WriteResult()
        { }

        public static WriteResult<T> Success(T data, int affectedCount = 0, string message = "写入成功")
        {
            return new WriteResult<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data,
                AffectedCount = affectedCount
            };
        }

        public new static WriteResult<T> Failure(string message, Exception exception = null, int affectedCount = 0)
        {
            return new WriteResult<T>
            {
                IsSuccess = false,
                Message = message,
                Exception = exception,
                AffectedCount = affectedCount
            };
        }

        public new static WriteResult<T> FromException(Exception ex, int affectedCount = 0)
        {
            return Failure($"写入过程中发生异常: {ex.Message}", ex, affectedCount);
        }
    }
}