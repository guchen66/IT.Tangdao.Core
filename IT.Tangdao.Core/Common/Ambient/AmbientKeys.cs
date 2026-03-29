using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Ambient
{
    internal sealed class AmbientKeys
    {
        public const string QueryContent = "TangdaoQueryContent";
        public const string FileType = "TangdaoFileType";
        public const string File = "TangdaoFile";
        public const string Solution = "TangdaoSolution";
        public const string Current = "TangdaoCurrent";

        /// <summary>
        /// 临时目录
        /// </summary>
        public const string Temp = "TangdaoTemp";

        /// <summary>
        /// 日期目录
        /// </summary>
        public const string DateDirectory = "TangdaoDateDirectory";

        /// <summary>
        /// 日期文件
        /// </summary>
        public const string DateFile = "TangdaoDateFile";
    }
}