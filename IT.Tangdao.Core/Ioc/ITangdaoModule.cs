using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Ioc
{
    public interface ITangdaoModule
    {
        /// <summary>
        ///  注册服务（必须）
        /// </summary>
        /// <param name="container"></param>
        void RegisterServices(ITangdaoContainer container);

        /// <summary>
        /// 模块初始化（可选）：此时 IOC 已 Build，可解析服务
        /// </summary>
        /// <param name="provider"></param>
        void OnInitialized(ITangdaoProvider provider);

        /// <summary>
        /// 模块优先级：数字越小越先注册；默认 0
        /// </summary>
        int Order => 0;

        /// <summary>
        /// 是否懒加载：true = 第一次用到该模块里的服务时才 RegisterServices
        /// </summary>
        bool Lazy => false;
    }
}