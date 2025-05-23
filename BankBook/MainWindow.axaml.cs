using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace BankBook
{
    public partial class MainWindow : Window
    {
        public string Greetings { get; } = "Hello world!";
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
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
}