using MAUI_POC.Views;

namespace MAUI_POC
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            //Routing.RegisterRoute(nameof(SettingPage), typeof(SettingPage));
            Routing.RegisterRoute(nameof(TaskAddUpdatePage), typeof(TaskAddUpdatePage));
            Routing.RegisterRoute(nameof(TodoTaskListPage), typeof(TodoTaskListPage));
            Routing.RegisterRoute(nameof(TaskDetailPage), typeof(TaskDetailPage));
            Routing.RegisterRoute(nameof(TakePicturePage), typeof(TakePicturePage));
        }
    }
}