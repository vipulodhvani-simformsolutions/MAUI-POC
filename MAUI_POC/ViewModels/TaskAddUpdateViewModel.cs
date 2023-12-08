using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_POC.Models.Enums;
using MAUI_POC.Services;
using MAUI_POC.Views;

namespace MAUI_POC.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class TaskAddUpdateViewModel : ObservableObject
    {
        ITodoTaskService _todoTaskService;

        public TaskAddUpdateViewModel(ITodoTaskService todoTaskService)
        {
            _todoTaskService = todoTaskService;
        }

        [ObservableProperty]
        int id;

        [ObservableProperty]
        string title;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        TodoTaskStatus status;

        [ObservableProperty]
        string validationMessageTitle;

        [ObservableProperty]
        string validationMessageDescription;

        [ObservableProperty]
        string pageLeble;

        [RelayCommand]
        async Task Submit()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(Title))
            {
                isValid = false;
                ValidationMessageTitle = "Title is required.";
            }
            else
            {
                ValidationMessageTitle = null;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                isValid = false;
                ValidationMessageDescription = "Description is required.";
            }
            else
            {
                ValidationMessageDescription = null;
            }

            if (isValid) 
            {

                var model = new Models.TodoTask() { Id = Id, Title = Title, Description = Description, Status = Status };
                if (model.Id > 0)
                {
                    await _todoTaskService.UpdateTask(model);
                }
                else
                {
                    await _todoTaskService.AddTask(model);
                }

                await Shell.Current.GoToAsync($"///{nameof(TodoTaskListPage)}");
            }
        }

        [RelayCommand]
        async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async Task GetTask(int taskId)
        {
            var todoTask = await _todoTaskService.GetTask(taskId);
            if (todoTask != null)
            {
                Id = todoTask.Id;
                Title = todoTask.Title;
                Description = todoTask.Description;
                Status = todoTask.Status;
            }
        }
    }
}
