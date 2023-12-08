using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace MAUI_POC.Converters
{
    public class EnumDisplayNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum enumValue)
            {
                FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
                if (fieldInfo != null && Attribute.GetCustomAttribute(fieldInfo, typeof(DisplayAttribute)) is DisplayAttribute attr)
                {
                    return attr.Name;
                }
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
