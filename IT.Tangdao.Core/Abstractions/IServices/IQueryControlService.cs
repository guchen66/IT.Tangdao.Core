using System.Windows;

namespace IT.Tangdao.Core.Abstractions
{
    public interface IQueryControlService
    {
        /// <summary>
        /// 查询一个面板的布局容器
        /// </summary>
        void QueryableLayoutControl(DependencyObject parent);
    }
}