using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoException
{
    public class ContainerErrorException:Exception
    {
        public ContainerErrorException(string message) : base(message)
        {
            
        }
    }
}
