using IT.Tangdao.Core.DaoParameters.Infrastructure;
using IT.Tangdao.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Providers
{
    public class DaoMenuProvider
    {
        private record Mode(string Path, Action<ITangdaoMenuItem> OnChange);
        private readonly Dictionary<string, Mode> _watchList = new();

        public virtual ITangdaoMenuItem Root { get; set; } = new TangdaoMenuItem()
        {
            Childs = new List<ITangdaoMenuItem>()
        };

        public event EventHandler? Changed;

        // 添加监控项
        public void Watch(string path, Action<ITangdaoMenuItem> onChange)
        {
            _watchList[path] = new Mode(path, onChange);
        }

        // 触发变更
        public void NotifyChange(string path)
        {
            if (_watchList.TryGetValue(path, out var mode))
            {
                var item = Root.Find(path);
                if (item != null)
                {
                    mode.OnChange?.Invoke(item);
                    Changed?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        // 通过路径获取或创建菜单项
        public ITangdaoMenuItem this[string path]
        {
            get => Root.Find(path) ?? throw new KeyNotFoundException($"Menu path '{path}' not found");
            set
            {
                var existing = Root.Find(path, true);
                if (existing != null)
                {
                    existing.Value = value.Value;
                    existing.Childs = value.Childs;
                    NotifyChange(path);
                }
            }
        }
    }
}