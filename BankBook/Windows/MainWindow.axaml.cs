using Avalonia.Controls;
using Avalonia.Interactivity;

namespace BankBook.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void menuFileExit_Click(object? sender, RoutedEventArgs e)
        {
            // quit BankBook
            Close();
        }
        public void menuHelpAbout_Click(object? sender, RoutedEventArgs e)
        {
            var wAboutWindow = new AboutWindow();
            wAboutWindow.ShowDialog(this);
        }

    }
}
