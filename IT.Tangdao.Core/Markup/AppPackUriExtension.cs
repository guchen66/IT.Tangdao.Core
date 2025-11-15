using IT.Tangdao.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace IT.Tangdao.Core.Markup
{
    public class AppPackUriExtension : MarkupExtension
    {
        public string TagName { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var uriContext = serviceProvider.GetService(typeof(IUriContext)) as IUriContext;
            if (uriContext?.BaseUri != null)
            {
                string viewName = System.IO.Path.GetFileNameWithoutExtension(uriContext.BaseUri.AbsolutePath);

                // 注册到PackUriProvider
                PackUriProvider.UriParses.TryAdd(viewName, uriContext.BaseUri);

                // 返回TagName或默认的viewName
                return string.IsNullOrEmpty(TagName) ? viewName : TagName;
            }

            return null;
        }
    }
}