using MAUI_POC.ViewModels;

namespace MAUI_POC.Views;

public partial class TaskAddUpdatePage : ContentPage
{
    private readonly TaskAddUpdateViewModel _viewModel;
    public TaskAddUpdatePage(TaskAddUpdateViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }
    protected override async void OnAppearing()
    {
        await _viewModel.GetTask(_viewModel.Id);
        _viewModel.PageLeble = _viewModel.Id > 0 ? "Update task" : "Add task";
        base.OnAppearing();
    }
}