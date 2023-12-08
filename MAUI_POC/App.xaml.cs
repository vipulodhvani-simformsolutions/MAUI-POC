using MAUI_POC.Views;

namespace MAUI_POC
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}