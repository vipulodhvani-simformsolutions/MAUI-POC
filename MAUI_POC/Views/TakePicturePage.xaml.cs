using MAUI_POC.ViewModels;

namespace MAUI_POC.Views;

public partial class TakePicturePage : ContentPage
{
    private readonly TakePictureViewModel _viewModel;

    public TakePicturePage(TakePictureViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.ShowSaveBtn = false;
        try
        {
            PermissionStatus wStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (wStatus != PermissionStatus.Granted)
            {
                await Shell.Current.DisplayAlert("Permission", "Please grant permission to access your location for displaying your current whereabouts.", "OK");
                return;
            }
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest()
                {
                    DesiredAccuracy = GeolocationAccuracy.High,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            if (location != null)
            {
                _viewModel.Latitude = location.Latitude;
                _viewModel.Longitude = location.Longitude;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Permission", ex.Message, "OK");
        }
    }
}