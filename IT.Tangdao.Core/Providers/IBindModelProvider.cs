using IT.Tangdao.Core.Mvvm;
using IT.Tangdao.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IT.Tangdao.Core.Extensions;
using IT.Tangdao.Core.Abstractions;
using IT.Tangdao.Core.Abstractions.FileAccessor;

namespace IT.Tangdao.Core.Providers
{
    public interface IBindModelProvider
    {
    }

    public interface IBindModelProvider<T> : IBindModelProvider
    {
        T Default { get; set; }
    }

    public class BindModelProvider<T> : DaoViewModelBase, IBindModelProvider<T>
    {
        public T Default { get; set; }
        private readonly IContentReader _readService;
        private string _objectPath;

        public string ObjectPath
        {
            get => _objectPath;
            set => _objectPath = value;
        }

        public event EventHandler<T> BindHandler;

        public BindModelProvider()
        {
            _readService = TangdaoApplication.Provider.GetService<IContentReader>();
            // var xmlData = _readService.Read(ObjectPath);
            // Default = XmlFolderHelper.Deserialize<T>(xmlData);
        }
    }
}