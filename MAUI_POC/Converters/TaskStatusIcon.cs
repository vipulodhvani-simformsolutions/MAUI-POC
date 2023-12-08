using CommunityToolkit.Maui.Converters;
using MAUI_POC.Helpers;
using MAUI_POC.Models.Enums;
using System.Globalization;

namespace MAUI_POC.Converters
{
    public class TaskStatusIcon : BaseConverterOneWay<TodoTaskStatus, string>
    {
        public override string DefaultConvertReturnValue { get; set; } = FontAwesomeFreeSolid400Helper.ListUl;

        public override string ConvertFrom(TodoTaskStatus value, CultureInfo culture)
        {
            if (value == TodoTaskStatus.Open)
                return "list_ul_solid.svg";

            if (value == TodoTaskStatus.InProgress)
                return "hourglass_half_regular.svg";

            if (value == TodoTaskStatus.Completed)
                return "check_double_solid.svg";

            return "list_ul_solid.svg";
        }
    }
}
