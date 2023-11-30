using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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

        public static string? ValidateNotEmpty(string propertyName,bool isShowPropetyName, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                if(isShowPropetyName)
                {
                    return $"{propertyName} is required.";
                } else
                {
                    return $"Required.";
                } 
            }

            return null;
        }

        public static string? ValidateCurrency(string propertyName, string input)
        {
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
            int maxDecimalPlaces = 2;
            int decimalPlaces = BitConverter.GetBytes(decimal.GetBits(amount)[3])[2];
            if (decimalPlaces > maxDecimalPlaces)
            {
                return $"{propertyName} format is wrong";
            }

            return null;
        }

        private static bool ValidateDecimal(string input)
        {
            if (decimal.TryParse(input, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out decimal result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ValidateImage(BitmapImage image)
        {
            if (!IsValidImage(image))
            {
                return "Required Image";
            }
            return null;
        }

        private static bool IsValidImage(BitmapImage image)
        {
            return image != null;
        }
    }
}
