using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.Results
{
    /// <summary>
    /// 读取操作结果
    /// </summary>
    public class ReadResult : IQueryableResult
    {
        public bool IsSuccess { get; protected set; }
        public string Message { get; protected set; }
        public DateTime Timestamp { get; } = DateTime.UtcNow;
        public Exception Exception { get; protected set; }
        public string RawValue { get; protected set; }

        protected ReadResult()
        { }

        public static ReadResult Success(string rawValue = null, string message = "读取成功")
        {
            return new ReadResult
            {
                IsSuccess = true,
                Message = message,
                RawValue = rawValue
            };
        }

        public static ReadResult Failure(string message, Exception exception = null, string rawValue = null)
        {
            return new ReadResult
            {
                IsSuccess = false,
                Message = message,
                Exception = exception,
                RawValue = rawValue
            };
        }

        public static ReadResult FromException(Exception ex, string rawValue = null)
        {
            return Failure($"读取过程中发生异常: {ex.Message}", ex, rawValue);
        }
    }
}