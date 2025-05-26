using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BankBook.Data;
using BankBook.Data.Models;
using Konscious.Security.Cryptography;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace BankBook.Windows;

public partial class LoginWindow : Window, INotifyPropertyChanged
{
    private BankBookContext db { get; set; }

    public LoginWindow()
    {
        InitializeComponent();
        DataContext = this;

        db = new BankBookContext();
    }

    private void LoginWindow_Opened(object? sender, EventArgs e)
    {
        fieldLoginAccountName.Focus();
    }

    private void buttonCancel_Click(object? sender, RoutedEventArgs e)
    {
        // close windows
        Close();
    }

    private void buttonLogin_Click(object? sender, RoutedEventArgs e)
    {
        // check fields
        if (fieldLoginAccountName.Text is null)
            fieldLoginAccountName.Text = string.Empty;
        if (fieldLoginAccountName.Text.Equals(string.Empty))
        {
            labelLoginErrorMessage.Text = "You must enter an account name";
            fieldLoginAccountName.Focus();
            return;
        }

        if (fieldLoginPassword.Text is null)
            fieldLoginPassword.Text = string.Empty;

        // search the user in db
        var result = db.Users.Where(x => x.Name == fieldLoginAccountName.Text);

        if (result.Any())
        {
            var userAccount = result.First();
            
            if (userAccount.PasswordHash is null)
                userAccount.PasswordHash = string.Empty;
            if (userAccount.PasswordHash.Equals(string.Empty))
            {
                // no password, login success
                var mMainWindow = new MainWindow();
                mMainWindow.Show();
                Close();
            }
            else            // check password hash
            {
                // check if password from form is empty
                if (fieldLoginPassword.Text.Equals(string.Empty))
                {
                    ClearLoginForm();
                    labelLoginErrorMessage.Text = "Bad credentials";
                    fieldLoginAccountName.Focus();
                    return;
                }
                // hash password from the form
                // TODO : get the salt from a configuration file and make it random on first startup
                var argon2 = new Argon2i(Encoding.UTF8.GetBytes(fieldLoginPassword.Text!));
                argon2.Salt = Encoding.UTF8.GetBytes("C3P123");
                argon2.DegreeOfParallelism = 2;
                argon2.MemorySize = 65536;
                argon2.Iterations = 4;

                if (userAccount.PasswordHash!.Equals(BitConverter.ToString(argon2.GetBytes(32)).Replace("-", "").ToLowerInvariant()))
                {
                    // login success
                    var wMainWindow = new MainWindow();
                    wMainWindow.Show();
                    Close();
                }
                else
                {
                    // account exists but password is wrong
                    ClearLoginForm();
                    labelLoginErrorMessage.Text = "Bad credentials";
                    fieldLoginAccountName.Focus();
                }
            }
        }
        else
        {
            // Account does not exist
            ClearLoginForm();
            labelLoginErrorMessage.Text = "Bad credentials";
            fieldLoginAccountName.Focus();
        }
    }

    private void buttonRegister_Click(object? sender, RoutedEventArgs e)
    {
        // check fields
        if (fieldRegisterAccountName.Text is null)
            fieldRegisterAccountName.Text = string.Empty;
        if (fieldRegisterAccountName.Text!.Equals(string.Empty))
        {
            labelRegisterErrorMessage.Text = "You must enter an account name";
            fieldRegisterAccountName.Focus();
            return;
        }

        if (fieldRegisterEmail.Text is null)
            fieldRegisterEmail.Text = string.Empty;

        if (fieldRegisterPassword.Text is null)
            fieldRegisterPassword.Text = string.Empty;

        // check if the account already exist
        var result = db.Users.Where(x => x.Name == fieldRegisterAccountName.Text).ToList();

        if (!result.Any())
        {
            // create the data model
            var userAccount = new UserAccount();
            userAccount.Name = fieldRegisterAccountName.Text;
            userAccount.Email = fieldRegisterEmail.Text;

            // hash password & record the account in the database
            if (!fieldRegisterPassword.Text!.Equals(String.Empty))
            {
                var argon2 = new Argon2i(Encoding.UTF8.GetBytes(fieldRegisterPassword.Text));
                argon2.Salt = Encoding.UTF8.GetBytes("C3P123");
                argon2.DegreeOfParallelism = 2;
                argon2.MemorySize = 65536;
                argon2.Iterations = 4;

                userAccount.PasswordHash = BitConverter.ToString(
                    argon2.GetBytes(32)).Replace("-", "").ToLowerInvariant();
            }
            else
                userAccount.PasswordHash = String.Empty;

            // write changes
            db.Users.Add(userAccount);

            if (db.SaveChanges() == 1)
            {
                var wMainWindow = new MainWindow();
                wMainWindow.Show();
                Close(); // success, close window
            }
            else
            {
                // can't write in the db
                // check permissions
                // TODO : add a log system
                labelRegisterErrorMessage.Text = "The account creation has failed";
            }

        }
        else
        {
            // account name is already used
            ClearRegisterForm();
            labelRegisterErrorMessage.Text = $"{fieldRegisterAccountName.Text} is already used";
            fieldRegisterAccountName.Focus();
        }
    }
    public new event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public void fieldLoginAccountName_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            fieldLoginPassword.Focus();
    }

    public void fieldLoginPassword_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            buttonLogin_Click(sender, e);
    }

    public void fieldRegisterAccountName_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            fieldRegisterEmail.Focus();
    }
    public void fieldRegisterEmail_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            fieldRegisterPassword.Focus();
    }

    public void fieldRegisterPassword_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            buttonRegister_Click(sender, e);
    }

    private void ClearLoginForm()
    {
        fieldLoginAccountName.Text = string.Empty;
        fieldLoginPassword.Text = string.Empty;
    }

    private void ClearRegisterForm()
    {
        fieldRegisterAccountName.Text = string.Empty;
        fieldRegisterEmail.Text = string.Empty;
        fieldRegisterPassword.Text = string.Empty;
    }
}
