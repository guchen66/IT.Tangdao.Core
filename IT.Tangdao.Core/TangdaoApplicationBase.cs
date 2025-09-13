using IT.Tangdao.Core.Attributes;
using IT.Tangdao.Core.Common;
using IT.Tangdao.Core.DaoEvents;
using IT.Tangdao.Core.Selectors;
using IT.Tangdao.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IT.Tangdao.Core
{
    public abstract class TangdaoApplicationBase : Application
    {
        private ITangdaoContainer Container;

        public ITangdaoProvider Provider;

        //  public TangdaoContext Context;

        public Assembly Assembly;

        protected TangdaoApplicationBase()
        {
            Container = TangdaoContainerBuilder.CreateContainer();
            Provider = TangdaoContainerBuilder.Builder();
            ServerLocator.InitContainer(Container);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitializeInternal();
        }

        private void InitializeInternal()
        {
            Register(Container);
            // 自动关联 View 和 ViewModel
            AutoWireViewAndViewModel();
            //  OnConfigure(Context);

            OnInitialized();
        }

        protected abstract void Register(ITangdaoContainer Container);

        /// <summary>
        /// 自动关联 View 和 ViewModel。
        /// </summary>
        private void AutoWireViewAndViewModel()
        {
            //获取当前运行的程序集
            Assembly assembly = GetAssembly(Assembly);

            // 使用 ViewToViewModelExtension 扫描标记了 ViewToViewModelAttribute 的类型
            var viewModelTypes = ViewToViewModelExtension.GetScanObject(assembly);

            foreach (var viewModelType in viewModelTypes)
            {
                ViewSelector.Build(Provider.Resolve(viewModelType), Provider);
            }
        }

        private void OnInitialized()
        {
            Window window = CreateWindow();
            if (window != null)
            {
                InitWindow(window);
            }
            // ChannelEvent.GetTangdaoContext(Provider);
        }

        private void InitWindow(Window window)
        {
            window.Show();
        }

        protected abstract Window CreateWindow();

        //  protected abstract void OnConfigure(TangdaoContext Context);

        protected virtual Assembly GetAssembly(Assembly assembly)
        {
            Assembly = assembly;
            return Assembly;
        }
    }
}