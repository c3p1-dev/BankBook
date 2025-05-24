using Avalonia.Controls;
using Avalonia.Interactivity;
using BankBook.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BankBook.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowLoginWindow_Click(object? sender, RoutedEventArgs e)
        {
            var wShowLogin = new BankBook.Windows.LoginWindow();
            wShowLogin.ShowDialog(this);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Login_Click(object? sender, RoutedEventArgs e)
        {
            // check if Db is empty
            var db = new BankBookContext();
            if (db.Users.Count() > 0)
                new LoginWindow().ShowDialog(this); // is not empty
            else
                new RegisterWindow().ShowDialog(this); // is empty
        }
    }
}