using Avalonia.Controls;
using BankBook.ViewModels;

namespace BankBook;

public partial class SplashScreenPageView : UserControl
{
    public SplashScreenPageView()
    {
        InitializeComponent();
    }

    private async void ShowSplash_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var splashScreen = new SplashScreenWindow();
        splashScreen.Show();

        await splashScreen.InitApp();
        splashScreen.Close();
    }
}