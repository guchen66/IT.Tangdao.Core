﻿using IT.Tangdao.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Infrastructure.Ambient
{
    public class CacheKey
    {
        public static string GetCacheKey(string path, DaoFileType type)
        {
            // 与 ContentQueryable 同一套根 key
            return string.Format("Content:{0}:{1}", path, type);
        }
    }
}