using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IT.Tangdao.Core.EventArg;

namespace IT.Tangdao.Core.Abstractions.Notices
{
    /// <summary>
    /// 通知发布者接口，用于发布通用通知给订阅者
    /// </summary>
    public interface ITangdaoPublisher : ITangdaoPublisher<object>, IDisposable
    {
    }
}