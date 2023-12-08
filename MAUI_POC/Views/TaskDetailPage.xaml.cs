using MAUI_POC.ViewModels;

namespace MAUI_POC.Views;

public partial class TaskDetailPage : ContentPage
{
    private readonly TaskDetailViewModel _viewModel;

    public TaskDetailPage(TaskDetailViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        await _viewModel.SetTaskDetail();
        barcodeImage.Barcode = _viewModel.TaskString;
        base.OnAppearing();
    }
}