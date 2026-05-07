using IT.Tangdao.Core.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Windows
{
    public interface IWindowBuilder
    {
        void UseGuard<TGuard>(Action<IWindowPipeline> windowPipeline) where TGuard : IWindowGuard;

        bool ExecuteAll();
    }
}