using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BankBook.Data;
using BankBook.Data.Models;
using Konscious.Security.Cryptography;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BankBook.Windows;

public partial class LoginWindow : Window, INotifyPropertyChanged
{
    public string? RegisterAccountName { get; set; }
    public string? RegisterEmail { get; set; }
    public string? RegisterPassword { get; set; }
    private string _registerErrorMessage = string.Empty;
    public string RegisterErrorMessage
    {
        get => _registerErrorMessage;
        set
        {
            if (_registerErrorMessage != value)
            {
                _registerErrorMessage = value;
                OnPropertyChanged(nameof(RegisterErrorMessage));
            }
        }
    }
    public string? LoginAccountName { get; set; }
    public string? LoginPassword { get; set; }
    public string LoginErrorMessage
    {
        get => _loginErrorMessage;
        set
        {
            if (_loginErrorMessage != value)
            {
                _loginErrorMessage = value;
                OnPropertyChanged(nameof(LoginErrorMessage));
            }
        }
    }
    private string _loginErrorMessage = string.Empty;
    private BankBookContext db { get; set; }

    public LoginWindow()
    {
        InitializeComponent();
        DataContext = this;

        db = new BankBookContext();
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        // close windows
        Close();
    }

    private void Login_Click(object? sender, RoutedEventArgs e)
    {
        // check login data
        var result = db.Users.Where(x => x.Name == LoginAccountName);

        if (result.Any())
        {
            var userAccount = result.First();

            // check password hash
            if (LoginPassword is null)
                LoginPassword = "_EMPTY_";
            using (var argon2 = new Argon2i(Encoding.UTF8.GetBytes(LoginPassword)))
            {
                argon2.Salt = Encoding.UTF8.GetBytes("C3P123");
                argon2.DegreeOfParallelism = 2;
                argon2.MemorySize = 65536;
                argon2.Iterations = 4;

                if (userAccount.PasswordHash is not null)
                {
                    if (userAccount.PasswordHash.Equals(BitConverter.ToString(argon2.GetBytes(32)).Replace("-", "").ToLowerInvariant()))
                    {
                        // Login success
                        var wMainWindow = new MainWindow();
                        wMainWindow.Show();
                        Close();
                    }
                    else
                    {
                        // Account exists but password is wrong
                        LoginErrorMessage = "Wrong credentials.";
                    }
                }
            }
        }
        else
        {
            // Account does not exist
            LoginErrorMessage = "Wrong credentials.";
        }
    }

    private void Register_Click(object? sender, RoutedEventArgs e)
    {
        // check if the account already exist
        var result = db.Users.Where(x => x.Name == RegisterAccountName).ToList();

        if (result.Count() == 0)
        {
            // hash password & record the account in the database
            if (RegisterPassword is null)
                RegisterPassword = "_EMPTY_";
            using (var argon2 = new Argon2i(Encoding.UTF8.GetBytes(RegisterPassword)))
            {
                argon2.Salt = Encoding.UTF8.GetBytes("C3P123");
                argon2.DegreeOfParallelism = 2;
                argon2.MemorySize = 65536;
                argon2.Iterations = 4;

                var userAccount = new UserAccount
                {
                    Name = RegisterAccountName,
                    Email = RegisterEmail,
                    PasswordHash = BitConverter.ToString(
                        argon2.GetBytes(32)).Replace("-", "").ToLowerInvariant()
                };

                db.Users.Add(userAccount);

                if (db.SaveChanges() == 1)
                {
                    var wMainWindow = new MainWindow();
                    wMainWindow.Show();
                    Close(); // success, close window
                }
                else
                    RegisterErrorMessage = "The account creation has failed";

            }
        }
        else
        {
            // account name is already used
            RegisterErrorMessage = $"{RegisterAccountName} is already used.";
            RegisterAccountName = String.Empty;
        }
    }
    public new event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
