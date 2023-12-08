using MAUI_POC.Models;
using MAUI_POC.ViewModels;

namespace MAUI_POC.Views;

public partial class TodoTaskListPage : ContentPage
{
    private readonly TodoTaskListViewModel _viewModel;
    public TodoTaskListPage(TodoTaskListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected async override void OnAppearing()
    {
        _viewModel.CurrentPage = 0;
        await _viewModel.GetTasks();
        base.OnAppearing();
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TaskAddUpdatePage));
    }
    private async void Button_Clicked_home(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(HomePage));
    }

    private void todoTaskCollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        var lastItem = _viewModel.TodoTasks[_viewModel.TodoTasks.Count - 1];
        var lastVisibalItem = _viewModel.TodoTasks[e.LastVisibleItemIndex];
        var firstVisibalItem = _viewModel.TodoTasks[e.FirstVisibleItemIndex];

        if (lastItem.Id == lastVisibalItem.Id && firstVisibalItem.Id != lastVisibalItem.Id)
        {
            _viewModel.CurrentPage++;
            Task.Run(async () => await _viewModel.GetTasks());
        }
    }

    private async void todoTaskCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null)
        {
            var model = e.CurrentSelection.FirstOrDefault() as TodoTask;

            await Shell.Current.GoToAsync($"{nameof(TaskAddUpdatePage)}?Id={model.Id}&Title={model.Title}&Description={model.Description}");
        }
    }
}