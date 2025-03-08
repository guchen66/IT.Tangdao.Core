using IT.Tangdao.Core.DaoDtos.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IT.Tangdao.Core.DaoDtos
{
    public  class MappingAlarmList:List<AlarmNotice>
    {
        public void Add(string viewName,string viewModelName)
        {
            RemoveAll((AlarmNotice it)=>it.ViewName.Equals(viewName));
            Add(new AlarmNotice 
            {
                ViewName = viewName,
                ViewModelName = viewModelName
            });
        }

        public new void Clear()
        {
            RemoveAll((AlarmNotice it) => true);
        }
    }
}
