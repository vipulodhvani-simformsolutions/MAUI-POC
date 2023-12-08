using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MAUI_POC.ViewModels
{
    public partial class TakePictureViewModel : ObservableObject
    {
        private const string SAVE_PHOTO_TXT = "Save photo";
        private const string SAVE_VIDEO_TXT = "Save video";
        public TakePictureViewModel()
        {
        }

        [ObservableProperty]
        bool showImageControl;

        [ObservableProperty]
        bool showVideoControl;

        [ObservableProperty]
        ImageSource imageSrc;

        [ObservableProperty]
        double latitude;

        [ObservableProperty]
        double longitude;

        [ObservableProperty]
        byte[] byteArray;

        [ObservableProperty]
        bool showSaveBtn;

        [ObservableProperty]
        bool showMediaElement;

        [ObservableProperty]
        string videoPath;

        [ObservableProperty]
        string saveBtnTxt = SAVE_PHOTO_TXT;

        [RelayCommand]
        async Task TakePic()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                PermissionStatus wStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (wStatus != PermissionStatus.Granted)
                {
                    await Shell.Current.DisplayAlert("Permission", "grant permission to access storage in order to save photo.", "OK");
                    return;
                }

                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                try
                {
                    if (photo != null)
                    {
                        ShowMediaElement = false;
                        using (var stream = await photo.OpenReadAsync())
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                await stream.CopyToAsync(ms);
                                ByteArray = ms.ToArray();
                            }

                            ImageSrc = ImageSource.FromStream(() => new MemoryStream(ByteArray));
                            ShowImageControl = true;

                            if (ByteArray.Length > 0)
                            {
                                ShowSaveBtn = true;
                                SaveBtnTxt = SAVE_PHOTO_TXT;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                    ShowImageControl = false;
                    ShowMediaElement = false;
                    ShowSaveBtn = false;
                }
            }
        }

        [RelayCommand]
        async Task SaveFile()
        {
            try
            {
                if (ByteArray != null && ByteArray.Length > 0)
                {
                    var folder = await FolderPicker.PickAsync(default);

                    if (!folder.IsSuccessful)
                    {
                        await Shell.Current.DisplayAlert("", "Please selete folder where you want to save the photo.", "OK");
                        return;
                    }
                    var DownloadPath = $"{folder.Folder.Path}/{Guid.NewGuid()}.png";

                    File.WriteAllBytes(DownloadPath, ByteArray);

                    await Toast.Make("File Saved!", ToastDuration.Short, 20).Show();
                }
                else if (!string.IsNullOrEmpty(VideoPath))
                {
                    var file = File.OpenRead(VideoPath);
                    if (file != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);
                            ByteArray = ms.ToArray();
                        }

                        var folder = await FolderPicker.PickAsync(default);

                        if (!folder.IsSuccessful)
                        {
                            await Shell.Current.DisplayAlert("", "Please selete folder where you want to save the video.", "OK");
                            return;
                        }
                        var DownloadPath = $"{folder.Folder.Path}/{Guid.NewGuid()}.mp4";

                        File.WriteAllBytes(DownloadPath, ByteArray);

                        await Toast.Make("File Saved!", ToastDuration.Short, 20).Show();
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "File not found, Please try again.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }

            ShowSaveBtn = false;
        }

        [RelayCommand]
        async Task PickPhoto()
        {
            ShowSaveBtn = false;
            PermissionStatus wStatus = await Permissions.RequestAsync<Permissions.StorageRead>();
            if (wStatus != PermissionStatus.Granted)
            {
                await Shell.Current.DisplayAlert("Permission", "grant permission to access storage in order to pick photo.", "OK");
                return;
            }

            FileResult photo = await MediaPicker.Default.PickPhotoAsync();
            try
            {
                if (photo != null)
                {
                    var stream = await photo.OpenReadAsync();
                    ImageSrc = ImageSource.FromStream(() => stream);
                    ShowMediaElement = false;
                    ShowImageControl = true;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                ShowImageControl = false;
                ShowMediaElement = false;
            }
        }

        [RelayCommand]
        async Task RecordVideo()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                PermissionStatus wStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (wStatus != PermissionStatus.Granted)
                {
                    await Shell.Current.DisplayAlert("Permission", "Please grant permission to access storage in order to save the video.", "OK");
                    return;
                }
                FileResult video = await MediaPicker.Default.CaptureVideoAsync();
                try
                {
                    ShowImageControl = false;
                    if (video != null)
                    {
                        VideoPath = video.FullPath;
                        ShowImageControl = false;
                        ShowMediaElement = true;
                        ShowSaveBtn = true;
                        SaveBtnTxt = SAVE_VIDEO_TXT;
                    }
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                    ShowImageControl = false;
                    ShowMediaElement = false;
                    ShowSaveBtn = false;
                }
            }
        }
    }
}
