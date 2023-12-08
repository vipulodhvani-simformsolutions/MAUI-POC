using Camera.MAUI;
using CommunityToolkit.Maui;
using MAUI_POC.Handler;
using MAUI_POC.Services;
using MAUI_POC.ViewModels;
using MAUI_POC.Views;
using Microsoft.Extensions.Logging;

namespace MAUI_POC
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    fonts.AddFont("Free-Regular-400.otf", "FAR");
                    fonts.AddFont("Free-Solid-900.otf", "FAS");
                })
                .UseMauiCameraView()
                .ConfigureMauiHandlers(handler =>
                {
                    FormHandler.RemoveBorders();
                });
            
            // pages
            builder.Services.AddTransient<HomePage>();
            //builder.Services.AddTransient<SettingPage>();
            builder.Services.AddTransient<TaskAddUpdatePage>();
            builder.Services.AddTransient<TodoTaskListPage>();  
            builder.Services.AddTransient<ScannerPage>();  
            builder.Services.AddTransient<TaskDetailPage>();  
            builder.Services.AddTransient<TakePicturePage>();  

            // view models
            builder.Services.AddTransient<DashboardViewModel>();
            builder.Services.AddTransient<TodoTaskListViewModel>();
            builder.Services.AddTransient<TaskAddUpdateViewModel>();
            builder.Services.AddTransient<ScannerViewModel>();
            builder.Services.AddTransient<TaskDetailViewModel>();
            builder.Services.AddTransient<TakePictureViewModel>();

            // services
            builder.Services.AddTransient<ITodoTaskService,TodoTaskService>();

#if DEBUG
            builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }
    }
}