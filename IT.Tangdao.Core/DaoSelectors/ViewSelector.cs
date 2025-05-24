using IT.Tangdao.Core.DaoAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IT.Tangdao.Core.DaoSelectors
{
    public static class ViewSelector
    {
        private static readonly Assembly _assembly = Assembly.GetEntryAssembly();

        /// <summary>
        /// 根据 ViewModel 类型自动关联 View 和 ViewModel。
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel 类型。</typeparam>
        /// <returns>关联后的 View 实例。</returns>
        public static Window ResolveView<TViewModel>() where TViewModel : class
        {
            // 获取 ViewModel 类型
            var viewModelType = typeof(TViewModel);

            // 查找 ViewToViewModelAttribute
            var attribute = viewModelType.GetCustomAttribute<ViewToViewModelAttribute>();
            if (attribute == null)
            {
                throw new InvalidOperationException($"ViewModel {viewModelType.Name} 未标记 ViewToViewModelAttribute。");
            }

            // 获取 View 类型
            var viewType = _assembly.GetTypes()
                .FirstOrDefault(t => t.Name == attribute.ViewName);
            if (viewType == null)
            {
                throw new InvalidOperationException($"未找到名为 {attribute.ViewName} 的 View。");
            }

            // 创建 View 和 ViewModel 实例
            var view = Activator.CreateInstance(viewType) as Window;
            var viewModel = Activator.CreateInstance(viewModelType);

            // 设置 DataContext
            view.DataContext = viewModel;

            return view;
        }

        public static Control? Build(object? data, ITangdaoProvider provider)
        {
            if (data is null)
                return null;

            var name = data.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
            var type = Type.GetType(name);

            if (type != null)
            {
                var control = (Control)provider.Resolve(type);
                control.DataContext = data;
                return control;
            }

            return new Control();
        }
    }
}