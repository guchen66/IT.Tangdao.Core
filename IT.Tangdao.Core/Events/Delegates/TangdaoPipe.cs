using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Events
{
    public delegate void TangdaoPipe<in T>(T msg);
}