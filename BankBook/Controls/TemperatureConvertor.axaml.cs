using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;

namespace BankBook.Controls;

public partial class TemperatureConvertor : UserControl
{
    public TemperatureConvertor()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        Debug.WriteLine($"Click! Ceslius={Celsius.Text}");
        if (double.TryParse(Celsius.Text, out double C))
        {
            var F = C * (9d / 5d) + 32;
            Farenheit.Text = F.ToString("0.0");
        }
        else
        {
            Celsius.Text = "0";
            Farenheit.Text = "0";
        }
    }

    private void Celsius_TextChanged(object? sender, TextChangedEventArgs e)
    {
        Button_OnClick(sender, e);
    }
}
