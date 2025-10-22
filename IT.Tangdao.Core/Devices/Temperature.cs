using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Devices
{
    /// <summary>
    /// 温度计
    /// </summary>
    public class Temperature
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Degree { get; set; }
    }
}