using IT.Tangdao.Core.DaoAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Extensions
{
    public class ScanneringExtension
    {
        public static IEnumerable<Type> GetScanObject<TModel>(TModel model)
        {
            return model.GetType().Assembly.GetTypes().Where(x => Attribute.IsDefined(x, typeof(ScanningAttribute)));
        }
    }

    public class ViewToViewModelExtension
    {
        // 原方法保持不变，供类库内部使用
        public static IEnumerable<Type> GetScanObject<TModel>(TModel model)
        {
            return model.GetType().Assembly.GetTypes()
                .Where(x => Attribute.IsDefined(x, typeof(ViewToViewModelAttribute)));
        }

        // 新增方法，允许主程序传递程序集
        public static IEnumerable<Type> GetScanObject(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(x => Attribute.IsDefined(x, typeof(ViewToViewModelAttribute)));
        }
    }
}