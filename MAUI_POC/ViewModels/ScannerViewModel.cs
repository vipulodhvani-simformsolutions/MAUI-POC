using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MAUI_POC.ViewModels
{
    public partial class ScannerViewModel : ObservableObject
    {
        public ScannerViewModel()
        {

        }

        [ObservableProperty]
        string barcodeFormat;

        [ObservableProperty]
        string barcodeResult;

        [RelayCommand]
        async Task CopyResult()
        {
            if (BarcodeResult != null)
            {
                await Clipboard.Default.SetTextAsync(BarcodeResult);

                var snackbar = Snackbar.Make("Copied to clipboard!", null, "OK", TimeSpan.FromSeconds(3), new SnackbarOptions
                {
                    BackgroundColor = Colors.Green,
                    TextColor = Colors.White
                });
                await snackbar.Show();
            }
            else
            {
                var snackbar = Snackbar.Make("Nothing to Copy", null, "OK", TimeSpan.FromSeconds(3), new SnackbarOptions
                {
                    BackgroundColor = Colors.Red,
                    TextColor = Colors.White
                });
                await snackbar.Show();
            }

        }

        [RelayCommand]
        async Task ClearedResult()
        {
            BarcodeFormat = null;
            BarcodeResult = null;


            var snackbar = Snackbar.Make("Cleared!", null, "OK", TimeSpan.FromSeconds(3), new SnackbarOptions
            {
                BackgroundColor = Colors.Green,
                TextColor = Colors.White
            });
            await snackbar.Show();
        }
    }
}
