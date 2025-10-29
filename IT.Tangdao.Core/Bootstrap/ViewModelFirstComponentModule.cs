using IT.Tangdao.Core.Abstractions.Contracts;
using IT.Tangdao.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IT.Tangdao.Core.Bootstrap
{
    internal class ViewModelFirstComponentModule : ITangdaoModule
    {
        public void RegisterServices(ITangdaoContainer container)
        {
        }

        public void OnInitialized(ITangdaoProvider provider)
        {
            foreach (var entry in provider.GetEntries()) // ✅ 直接拿快照
            {
                var vmType = entry.ServiceType;
                if (!typeof(IViewModel).IsAssignableFrom(vmType)) continue;

                var viewName = vmType.Name.Replace("Model", "");
                var viewType = vmType.Assembly.GetTypes()
                                      .FirstOrDefault(t => t.Name == viewName &&
                                                      typeof(FrameworkElement).IsAssignableFrom(t));
                if (viewType == null) continue;

                var template = new DataTemplate
                {
                    DataType = vmType,
                    VisualTree = new FrameworkElementFactory(viewType)
                };
                Application.Current.Resources.Add(template.DataType, template);
            }
        }
    }
}