using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.Helper
{
    public class ValidationHelper
    {
        public static string? ValidateFloat(string propertyName, float value, float minValue = float.MinValue, float maxValue = float.MaxValue)
        {
            if(value < minValue || value > maxValue)
            {
                return $"{propertyName} is must be float.";
            }
            return null;
        }

        public static string? ValidateNotEmpty(string propertyName, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return $"{propertyName} is required.";
            }

            return null; // Không có lỗi
        }

        public static string? ValidateCurrency(string propertyName, string input)
        {
            // Sử dụng hàm ValidateDecimal để kiểm tra giá trị decimal
            if (string.IsNullOrEmpty(input))
            {
                return "Required!";
            }
            if (!ValidateDecimal(input))
            {
                return $"{propertyName} Must be decimal.";
            }

            // Kiểm tra giá trị tiền tệ có lớn hơn hoặc bằng 0 không
            decimal amount = decimal.Parse(input, CultureInfo.InvariantCulture);
            if (amount < 0)
            {
                return $"{propertyName} must be greater than or equal to 0.";
            }

            // Kiểm tra giá trị tiền tệ không được quá lớn
            decimal maxAllowedAmount = 1000000000;
            if (amount > maxAllowedAmount)
            {
                return $"{propertyName} is too high.";
            }

            // Kiểm tra giá trị tiền tệ không được có quá nhiều chữ số thập phân
            int maxDecimalPlaces = 2; // Số này chỉ là ví dụ, bạn có thể thay đổi tùy theo yêu cầu
            int decimalPlaces = BitConverter.GetBytes(decimal.GetBits(amount)[3])[2];
            if (decimalPlaces > maxDecimalPlaces)
            {
                return $"{propertyName} format is wrong";
            }

            return null;
        }

        private static bool ValidateDecimal(string input)
        {
            // Sử dụng TryParse để kiểm tra giá trị decimal
            if (decimal.TryParse(input, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out decimal result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
