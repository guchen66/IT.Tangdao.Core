﻿using IT.Tangdao.Core.DaoDtos;
using IT.Tangdao.Core.DaoEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin.IServices
{
    public interface IWriteService
    {
        void WriteString(string path, string content, DaoFileType daoFileType = DaoFileType.None);

        Task<IWriteResult> WriteAsync(string path, string content, DaoFileType daoFileType = DaoFileType.None);

        void WriteEntityToXml<TEntity>(TEntity entity, string path, DaoFileType daoFileType) where TEntity : class, new();

        void WriteFilter(string path, Expression<Func<string, bool>> func);
    }
}