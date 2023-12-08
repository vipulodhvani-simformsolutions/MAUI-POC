using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_POC.Models;
using MAUI_POC.Services;
using System.Text.Json;

namespace MAUI_POC.ViewModels
{
    [QueryProperty(nameof(TaskString), nameof(TaskString))]
    public partial class TaskDetailViewModel: ObservableObject
    {
       
        private readonly ITodoTaskService _todoTaskService;
        public TaskDetailViewModel(ITodoTaskService todoTaskService)
        {
            _todoTaskService = todoTaskService;
        }

        [ObservableProperty]
        string taskString;

        [ObservableProperty]
        TodoTask taskDetail;

        public async Task SetTaskDetail()
        {
            var teskD = JsonSerializer.Deserialize<TodoTask>(TaskString);
            TaskDetail = await _todoTaskService.GetTask(teskD.Id);
        }

        [RelayCommand]
        async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
