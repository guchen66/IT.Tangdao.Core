using IT.Tangdao.Core.DaoParameters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoSelectors
{
    public class DateTimeSelector
    {
        private static readonly Lazy<TangdaoClock> _dateTimeItem = new Lazy<TangdaoClock>(() => new TangdaoClock());

        public static TangdaoClock Instance => _dateTimeItem.Value;

        // 可选：提供一个静态属性用于绑定
        public static DateTime CurrentDateTime => Instance.CurrentDate;
    }
}