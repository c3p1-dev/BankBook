using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace BankBook
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                //desktop.MainWindow = new Windows.MainWindow();
                // Start app with login window
                desktop.MainWindow = new Windows.LoginWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
