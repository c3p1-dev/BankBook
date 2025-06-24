using Avalonia.Controls;
using System.Diagnostics;

namespace BankBook;

public partial class AboutPageView : UserControl
{
    public AboutPageView()
    {
        InitializeComponent();
    }

    private void OpenUrl(string url)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }

    private void GitHub_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        OpenUrl("https://github.com/c3p1-dev/");

    }

    private void C3P1_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        OpenUrl("https://c3p1.net/");
    }
}