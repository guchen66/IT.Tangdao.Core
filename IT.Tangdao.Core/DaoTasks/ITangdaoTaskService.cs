using IT.Tangdao.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoTasks
{
    public interface ITangdaoTaskService
    {
        Task StartAsync(IProgress<IAddTaskItem> progress);

        void Pause();

        void Resume();

        void Stop();
    }
}