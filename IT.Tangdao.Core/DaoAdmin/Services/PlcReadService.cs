using IT.Tangdao.Core.DaoAdmin.IServices;
using IT.Tangdao.Core.DaoDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.Services
{
    public class PlcReadService : IPlcReadService
    {
        public Task<TangdaoResponse> ReadAsync(string path)
        {
            throw new NotImplementedException();
        }
    }
}
