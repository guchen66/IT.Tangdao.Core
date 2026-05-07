using IT.Tangdao.Core.Attributes;
using IT.Tangdao.Core.Enums;
using IT.Tangdao.Core.Events;
using IT.Tangdao.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IT.Tangdao.Core.Windows
{
    public interface IWindowPipeline
    {
        IWindowGuard Guard { get; set; }

        /// <summary>
        /// 配置登录窗口类型
        /// </summary>
        IWindowPipeline UseLogin<TWindow>() where TWindow : Window;

        /// <summary>
        /// 配置公共窗口类型
        /// </summary>
        IWindowPipeline UseConfigure<TWindow>() where TWindow : Window;

        /// <summary>
        /// 设置是否允许取消（默认true）
        /// </summary>
        IWindowPipeline SetCancel(bool allow);

        /// <summary>
        /// 配置登录成功后的回调
        /// </summary>
        IWindowPipeline OnSuccess(Action action);

        /// <summary>
        /// 配置登录失败/取消后的回调
        /// </summary>
        IWindowPipeline OnFailure(Action action);

        ShowMode ShowMode { get; set; }

        bool Execute();
    }

    public static class WindowPipelineExtension
    {
        //public static bool Execute(this IWindowPipeline windowPipeline)
        //{
        //    return true;
        //}
    }
}