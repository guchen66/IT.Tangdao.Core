using IT.Tangdao.Core.DaoDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.IServices
{
    public interface IPlcReadService
    {
        Task<TangdaoResponse> ReadAsync(string path);
    }
}
