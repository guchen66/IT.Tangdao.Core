using IT.Tangdao.Core.DaoAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Extensions
{
    public class ScanneringExtension
    {
        public static IEnumerable<Type> GetScanObject<TModel>(TModel model)
        {
            return model.GetType().Assembly.GetTypes().Where(x=>Attribute.IsDefined(x,typeof(ScanningAttribute)));
        }
    }



    public class ViewToViewModelExtension
    {
        public static IEnumerable<Type> GetScanObject<TModel>(TModel model)
        {
            return model.GetType().Assembly.GetTypes().Where(x => Attribute.IsDefined(x, typeof(ViewToViewModelAttribute)));
        }
    }
}
