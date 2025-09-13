﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Abstractions.Results
{
    /// <summary>
    /// 泛型操作结果接口
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public interface IQueryableResult<T> : IQueryableResult
    {
        T Data { get; }
    }
}