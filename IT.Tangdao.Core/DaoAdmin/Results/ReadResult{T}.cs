using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.Results
{
    /// <summary>
    /// 泛型读取结果
    /// </summary>
    /// <typeparam name="T">读取的数据类型</typeparam>
    public class ReadResult<T> : ReadResult, IQueryableResult<T>
    {
        public T Data { get; private set; }

        public static ReadResult<T> Success(T data, string rawValue = null, string message = "读取成功")
        {
            return new ReadResult<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data,
                RawValue = rawValue
            };
        }

        public new static ReadResult<T> Failure(string message, Exception exception = null, string rawValue = null)
        {
            return new ReadResult<T>
            {
                IsSuccess = false,
                Message = message,
                Exception = exception,
                RawValue = rawValue
            };
        }

        public new static ReadResult<T> FromException(Exception ex, string rawValue = null)
        {
            return Failure($"读取过程中发生异常: {ex.Message}", ex, rawValue);
        }

        // 隐式转换操作符，方便使用
        public static implicit operator ReadResult<T>(T data)
        {
            return Success(data);
        }
    }
}