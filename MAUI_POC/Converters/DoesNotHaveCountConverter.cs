using CommunityToolkit.Maui.Converters;
using System.Globalization;

namespace MAUI_POC.Converters
{
    public class DoesNotHaveCountConverter : BaseConverterOneWay<int, bool>
    {
        public override bool DefaultConvertReturnValue { get; set; } = false;

        public override bool ConvertFrom(int value, CultureInfo culture)
        {
            return value <= 0;
        }
    }
}
