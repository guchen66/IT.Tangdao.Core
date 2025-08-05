using IT.Tangdao.Core.DaoEvents;
using IT.Tangdao.Core.DaoIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAdmin
{
    public interface IRouter
    {
        public bool CanGoBack { get; }
        public bool CanGoForward { get; }

        // 导航到指定页面类型
        void NavigateTo<T>(object parameters) where T : ITangdaoPage;

        // 导航到指定路由
        void NavigateTo(string route, object parameters);

        // 导航历史操作
        void GoBack();

        void GoForward();

        // 当前页面
        ITangdaoPage CurrentPage { get; }

        // 路由变化事件
        event EventHandler<RouteChangedEventArgs> RouteChanged;

        // 注册路由
        void RegisterRoute(string route, Func<ITangdaoPage> pageFactory);

        void RegisterPage<T>() where T : ITangdaoPage, new();
    }
}