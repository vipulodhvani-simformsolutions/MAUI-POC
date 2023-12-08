using System.ComponentModel.DataAnnotations;

namespace MAUI_POC.Models.Enums
{
    public enum TodoTaskStatus
    {
        [Display(Name ="Open")]
        Open,

        [Display(Name = "In-Progress")]
        InProgress,

        [Display(Name = "Completed")]
        Completed
    }
}
