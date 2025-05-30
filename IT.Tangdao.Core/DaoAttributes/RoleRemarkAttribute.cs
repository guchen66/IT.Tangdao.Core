﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.DaoAttributes
{
    /// <summary>
    /// Remark特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RoleRemarkAttribute : Attribute
    {
        public int Remark { get; private set; }

        public RoleRemarkAttribute(int remark)
        {
            this.Remark = remark;
        }
    }
}
