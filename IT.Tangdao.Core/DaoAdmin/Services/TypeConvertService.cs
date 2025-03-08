using IT.Tangdao.Core.DaoAdmin.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IT.Tangdao.Core.Extensions;
using IT.Tangdao.Core.DaoDtos.Globals;
using IT.Tangdao.Core.DaoException;
using System.Reflection;
namespace IT.Tangdao.Core.DaoAdmin.Services
{
    public class TypeConvertService : ITypeConvertService
    {
        public T Converter<T>(string name) where T : class,new() 
        {
            T type=new T();

            if (!typeof(T).IsHasConstructor())
            {
                throw new MissingParameterlyConstructorException("缺少无参构造器");
            }
            if (name != type.GetType().Name)
            {
                throw new ImproperNamingException($"{name}命名不规范");
            }
            return type;
        }
    }
}
