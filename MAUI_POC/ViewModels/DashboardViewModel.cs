using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_POC.Services;
using MAUI_POC.Views;

namespace MAUI_POC.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        [ObservableProperty]
        int openTask;

        [ObservableProperty]
        int inProhressTask;

        [ObservableProperty]
        int completedTask;

        ITodoTaskService _todoTaskService;
        public DashboardViewModel(ITodoTaskService todoTaskService)
        {
            _todoTaskService = todoTaskService;
        }

        [RelayCommand]
        async Task GotoTaskList()
        {
            await Shell.Current.GoToAsync($"//{nameof(TodoTaskListPage)}");
        }
        public async Task GetTaskCount()
        {
            var tasks = await _todoTaskService.GetTasks();
            if (tasks != null && tasks.Any())
            {
                OpenTask = tasks.Count(x => x.Status == Models.Enums.TodoTaskStatus.Open);
                InProhressTask = tasks.Count(x => x.Status == Models.Enums.TodoTaskStatus.InProgress);
                CompletedTask = tasks.Count(x => x.Status == Models.Enums.TodoTaskStatus.Completed);
            }
        }
    }
}
