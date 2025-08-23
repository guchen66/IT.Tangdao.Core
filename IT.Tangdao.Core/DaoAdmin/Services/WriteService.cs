using IT.Tangdao.Core.DaoAdmin.IServices;
using IT.Tangdao.Core.DaoAdmin.Results;
using IT.Tangdao.Core.DaoDtos;
using IT.Tangdao.Core.DaoDtos.Globals;
using IT.Tangdao.Core.DaoEnums;
using IT.Tangdao.Core.Extensions;
using IT.Tangdao.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.Services
{
    public class WriteService : IWriteService
    {
        public void WriteString(string path, string content, DaoFileType daoFileType = DaoFileType.None)
        {
            if (daoFileType == DaoFileType.None)
            {
                daoFileType = DaoFileType.Txt;
            }
            path.UseFileWriteToTxt(content);
        }

        public async Task<WriteResult> WriteAsync(string path, string content, DaoFileType daoFileType = DaoFileType.None)
        {
            if (daoFileType == DaoFileType.None)
            {
                daoFileType = DaoFileType.Txt;
            }
            await new TimeSpan(1000);
            path.UseFileWriteToTxt(content);
            return WriteResult<string>.Success(content);
        }

        public void WriteFilter(string path, Expression<Func<string, bool>> func)
        {
            throw new NotImplementedException();
        }

        public void WriteEntityToXml<TEntity>(TEntity entity, string path) where TEntity : class, new()
        {
            if (path != null)
            {
                var info = XmlFolderHelper.SerializeXML<TEntity>(entity);
                WriteString(path, info);
            }
            else
            {
                TangdaoGuards.ThrowIfNull(entity);
            }
        }
    }
}