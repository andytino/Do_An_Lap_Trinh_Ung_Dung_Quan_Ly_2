
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.Helper
{
    public class formatHelper
    {
        public static string FormatCurrency(float amount)
        {
            // Sử dụng CultureInfo để định dạng số tiền thành chuỗi tiền tệ
            CultureInfo cultureInfo = new CultureInfo("en-US");
            return string.Format(cultureInfo, "{0:C0}", amount);
        }
    }
}
