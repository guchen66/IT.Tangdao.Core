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
        public static int Square(this int value) => (int)InternalSquare(value);

        public static double Square(this double value) => InternalSquare(value);

        public static int MathToAbs(this int value) => (int)InternalMathToAbs(value);

        public static double MathToAbs(this double value) => InternalMathToAbs(value);

        private static double InternalSquare(double value) => value * value;

        private static double InternalMathToAbs(double value)
        {
            return Math.Abs(value);
        }
    }
}