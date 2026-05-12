using IT.Tangdao.Core.Abstractions.Loggers;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Events;
using IT.Tangdao.Core.Ioc;
using IT.Tangdao.Core.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IT.Tangdao.Core.Windows
{
    internal class WindowAction
    {
        private readonly IEventAggregator _eventAggregator;

        public WindowAction(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe<WinEventBase>(ExecuteShow);
        }

        private void ExecuteShow(WinEventBase @event)
        {
            //1、进行数据传输
            ConfigureGuardAware(@event);

            //2、窗口未设置打开，直接退出
            if (!@event.IsShow)
            {
                return;
            }

            // 3、显示窗口
            Window window = ServiceLocator.Default.GetService(@event.WindowType) as Window;

            // 根据模式显示
            if (@event.ShowMode == ShowMode.ShowDialog)
            {
                window.Closed += (obj, sender) =>
                {
                    if (window.DialogResult != true)
                    {
                        Environment.Exit(0);
                    }
                };
                bool? result = window.ShowDialog();

                if (result == true)
                    @event.SucessAction?.Invoke();
                else
                    @event.FailureAction?.Invoke();
            }
            else
            {
                window.Show();
                // 非模态窗口：回调不会自动触发，可通过 Closed 事件实现
                if (@event.SucessAction != null || @event.FailureAction != null)
                {
                    window.Closed += (s, e) => @event.SucessAction?.Invoke();
                }
            }
        }

        private void ConfigureGuardAware(WinEventBase @event)
        {
            Type vmType = ViewToViewModelLocator.FindViewModelType(@event.WindowType);

            var vm = ServiceLocator.Default.GetService(vmType);
            if (vm is IGuardAware guardAware)
            {
                guardAware.Response(@event.Context);
            }
        }
    }
}