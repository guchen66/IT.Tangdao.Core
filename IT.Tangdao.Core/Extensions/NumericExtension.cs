using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Extensions
{
    /// <summary>
    /// 为所有数字类型提供扩展的方法
    /// </summary>
    public static class NumericExtension
    {
        public static int Square(this int value) => (int)SquareCore(value);

        public static double Square(this double value) => SquareCore(value);

        public static int MathToAbs(this int value) => (int)MathToAbsCore(value);

        public static double MathToAbs(this double value) => MathToAbsCore(value);

        private static double SquareCore(double value) => value * value;

        private static double MathToAbsCore(double value)
        {
            return Math.Abs(value);
        }
    }
}