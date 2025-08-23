using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.Results
{
    /// <summary>
    /// 写入操作结果
    /// </summary>
    public class WriteResult : IQueryableResult
    {
        public bool IsSuccess { get; protected set; }
        public string Message { get; protected set; }
        public DateTime Timestamp { get; } = DateTime.UtcNow;
        public Exception Exception { get; protected set; }
        public int AffectedCount { get; protected set; }

        public WriteResult()
        { }

        public static WriteResult Success(int affectedCount = 0, string message = "写入成功")
        {
            return new WriteResult
            {
                IsSuccess = true,
                Message = message,
                AffectedCount = affectedCount
            };
        }

        public static WriteResult Failure(string message, Exception exception = null, int affectedCount = 0)
        {
            return new WriteResult
            {
                IsSuccess = false,
                Message = message,
                Exception = exception,
                AffectedCount = affectedCount
            };
        }

        public static WriteResult FromException(Exception ex, int affectedCount = 0)
        {
            return Failure($"写入过程中发生异常: {ex.Message}", ex, affectedCount);
        }
    }
}