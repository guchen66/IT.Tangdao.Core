﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace IT.Tangdao.Core.DaoConverters
{
    public class EnumBindSourceValueConverter : MarkupExtension
    {
        private Type _enumType;

        public Type EnumType
        {
            get => _enumType;
            set => _enumType = value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("类型必须是枚举类型");
            }
            var actualType = Nullable.GetUnderlyingType(_enumType) ?? _enumType;
            var enumType = Enum.GetValues(_enumType);
            if (actualType == _enumType)
            {
                return enumType;
            }
            var tempArray = Array.CreateInstance(actualType, enumType.Length + 1);
            return tempArray;
        }
    }
}