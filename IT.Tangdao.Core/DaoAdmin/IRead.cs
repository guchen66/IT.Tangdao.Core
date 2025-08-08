using IT.Tangdao.Core.DaoAdmin.IServices;
using IT.Tangdao.Core.DaoEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IT.Tangdao.Core.DaoAdmin
{
    /// <summary>
    /// 高级的读取接口，可以读取除了txt文件，xml文件之外的其他类型
    /// </summary>
    public interface IRead
    {
        string XMLData { get; set; }

        string JsonFileName { get; set; }

        IReadResult SelectNode(string text);

        IReadResult SelectNodes(string path);

        IReadResult<List<T>> SelectNodes<T>() where T : new();

        IReadResult<List<T>> SelectNodes<T>(string rootElement, Func<XElement, T> selector);

        IReadResult SelectKeys();

        /// <summary>
        /// 跟据Key读取Value
        /// 用于数组读取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IReadResult SelectValue(string key);

        /// <summary>
        /// 读取Json对象
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="Result"></param>
        /// <returns></returns>
       // IReadResult SelectJsonObject<TResult>(TResult @Result);

        IReadResult SelectConfig(string section);

        IReadResult SelectCustomConfig(string configName, string section);

        public IRead this[string readObject] { get; }

        public IRead this[int readIndex] { get; }

        public void Load(string data);
    }
}