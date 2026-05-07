using IT.Tangdao.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Events
{
    public abstract class EventBase
    {
    }

    public class WinEventBase : EventBase
    {
        public Type WindowType { get; set; }

        public ShowMode Mode { get; set; }

        public Action SucessAction { get; set; }

        public Action FailureAction { get; set; }
    }
}