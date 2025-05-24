using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IT.Tangdao.Core.DaoValitations
{
    public class LoginValidate : ValidationRule
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string username = value as string;
            if (string.IsNullOrEmpty(username))
            {
                return new ValidationResult(false, "用户名密码不能为空");
            }
            return ValidationResult.ValidResult;
        }
    }
}