using System.Globalization;

namespace MAUI_POC.Converters
{
    class StringTruncateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text && parameter is string lengthParam && int.TryParse(lengthParam, out int maxLength))
            {
                if (text.Length > maxLength)
                    return text.Replace("\n", "").Substring(0, maxLength) + "...";
                else
                    return text.Replace("\n", "");
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
