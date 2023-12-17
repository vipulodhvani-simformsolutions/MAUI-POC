using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_POC.Models;
using MAUI_POC.Services;
using MAUI_POC.Views;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace MAUI_POC.ViewModels
{
    public partial class TodoTaskListViewModel : ObservableObject
    {
        ObservableCollection<TodoTask> todoTasks = new ObservableCollection<TodoTask>();
        public ObservableCollection<TodoTask> TodoTasks { get { return todoTasks; } }

        [ObservableProperty]
        bool isRefresh = false;

        [ObservableProperty]
        int currentPage = 0;

        int pageSize = 10;
        bool isBusy;

        ITodoTaskService _todoTaskService;
        public TodoTaskListViewModel(ITodoTaskService todoTaskService)
        {
            CurrentPage = 0;
            _todoTaskService = todoTaskService;            
        }

        public async Task GetTasks()
        {
            if(isBusy) return;

            isBusy = true;
            await Task.Delay(1000);
            var tasks = await _todoTaskService.GetTasks(CurrentPage, pageSize);
            if (tasks != null && tasks.Any())
            {
                if (TodoTasks.Count != 0 && CurrentPage == 0)
                {
                    todoTasks = new ObservableCollection<TodoTask>();
                }
                CurrentPage++;
                foreach (var task in tasks)
                {
                    todoTasks.Add(task);
                }
            }
            else if(CurrentPage == 0 && tasks?.Any() == false)
            {
                todoTasks = new ObservableCollection<TodoTask>();
            }
            OnPropertyChanged(nameof(TodoTasks));

            isBusy = false;
        }

        [RelayCommand]
        void SubmitTask()
        {
            Shell.Current.GoToAsync(nameof(TaskAddUpdatePage));
        }

        [RelayCommand]
        async Task DeleteTask(int id)
        {
            var isConfirm = await Shell.Current.DisplayAlert("Delete", "Are you sure want to delete this task?", "Yes", "Cancel");
            if (isConfirm)
            {
                CurrentPage = 0;
               await _todoTaskService.DeleteTask(id);
               await GetTasks();
            }
        }

        [RelayCommand]
        async Task EditTask(int id)
        {
            var model = await _todoTaskService.GetTask(id);
            await Shell.Current.GoToAsync($"{nameof(TaskAddUpdatePage)}?Id={model.Id}");
        }

        [RelayCommand]
        async Task Refresh()
        {
            IsRefresh = true;
            CurrentPage = 0;
            await GetTasks();
            IsRefresh = false;
        }

        [RelayCommand]
        async Task LoadMore()
        {
            await GetTasks();
        }

        [RelayCommand]
        async Task ViewTask(TodoTask task)
        {
            var taskString = JsonSerializer.Serialize(task);
            await Shell.Current.GoToAsync($"{nameof(TaskDetailPage)}?TaskString={taskString}");
        }
    }
}
