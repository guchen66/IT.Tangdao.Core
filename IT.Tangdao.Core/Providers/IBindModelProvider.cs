using IT.Tangdao.Core.DaoAdmin.IServices;
using IT.Tangdao.Core.DaoMvvm;
using IT.Tangdao.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IT.Tangdao.Core.Extensions;

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
        private readonly IReadService _readService;
        private string _objectPath;

        public string ObjectPath
        {
            get => _objectPath;
            set => _objectPath = value;
        }

        public event EventHandler<T> BindHandler;

        public BindModelProvider()
        {
            _readService = ServerLocator.Current.Resolve<IReadService>();
            var xmlData = _readService.Read(ObjectPath);
            Default = XmlFolderHelper.Deserialize<T>(xmlData);
        }
    }

    public class Test : BindModelProvider<Student>
    {
        private Student _student;

        public Student Student
        {
            get => _student;
            set => SetProperty(ref _student, value);
        }

        public Test()
        {
            Student = Default;
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}