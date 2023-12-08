using MAUI_POC.Models.Enums;
using SQLite;
using System.Runtime.Serialization;

namespace MAUI_POC.Models
{
    public class TodoTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoTaskStatus Status { get; set; }

        [Ignore]
        public string ImageUrl { get { return imagePath(Status); } }

        public string imagePath(TodoTaskStatus status)
        {
            switch (status)
            {
                case TodoTaskStatus.Open:
                    return "list_solid.svg";
                case TodoTaskStatus.InProgress:
                    return "circle_half_stroke_solid.svg";
                case TodoTaskStatus.Completed:
                    return "circle_check_solid.svg";
                default:
                    break;
            }
            return "circle_notch_solid.svg";
        }
    }
}
