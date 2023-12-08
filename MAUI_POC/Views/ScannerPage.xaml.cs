using MAUI_POC.ViewModels;

namespace MAUI_POC.Views;

public partial class ScannerPage : ContentPage
{
    private readonly ScannerViewModel _viewModel;

    public ScannerPage(ScannerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        if (cameraView.Cameras.Count > 0)
        {
            cameraView.Camera = cameraView.Cameras[0];
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await cameraView.StopCameraAsync();
                await cameraView.StartCameraAsync();
            });
        }
    }

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (cameraView.Cameras.Count > 0)
        {
            cameraView.Camera = cameraView.Cameras[0];
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await cameraView.StopCameraAsync();
                await cameraView.StartCameraAsync();
            });
        }
    }

    private void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        if (!string.Equals(_viewModel.BarcodeResult, args.Result[0].Text))
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                _viewModel.BarcodeFormat = args.Result[0].BarcodeFormat.ToString();
                _viewModel.BarcodeResult = args.Result[0].Text;
                await cameraView.StopCameraAsync();
                Vibration.Default.Vibrate();
                await DisplayAlert($"Scanned {args.Result[0].BarcodeFormat}", args.Result[0].Text, "OK");
                await cameraView.StartCameraAsync();
            });
        }
    }
}