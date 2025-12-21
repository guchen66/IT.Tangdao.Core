using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Abstractions.Navigation
{
    public interface ISingleNavigateView
    {
        string ViewName { get; }
        int DisplayOrder { get; }
    }
}