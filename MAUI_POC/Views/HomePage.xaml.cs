using MAUI_POC.ViewModels;

namespace MAUI_POC.Views;

public partial class HomePage : ContentPage
{
	private readonly DashboardViewModel _viewModel;
	public HomePage(DashboardViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.GetTaskCount();
    }
}