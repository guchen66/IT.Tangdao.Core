﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core
{
    public interface ITangdaoProvider : ITangdaoProviderBuilder
    {
        object Resolve(Type type);

        object Resolve(Type type, params object[] impleType);
    }
}