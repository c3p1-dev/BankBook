using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BankBook.Data;
using BankBook.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;
using System.Linq;

namespace BankBook.Windows;

public partial class RegisterWindow : Window
{
    public string? AccountName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ErrorMessage { get; set; }

    private BankBookContext db { get; set; }
    public RegisterWindow()
    {
        InitializeComponent();
        DataContext = this;

        // init database connexion
        db = new BankBookContext();
    }

    private void CreateAccount_Click(object? sender, RoutedEventArgs e)
    {
        // check if the account already exist
        var result = db.Users.Where(x => x.Name == AccountName).ToList();

        if (result.Count() == 0)
        {
            if (Password is null)
                Password = "_EMPTY_";
            // hash password & record the account in the database
            using (var argon2 = new Argon2i(Encoding.UTF8.GetBytes(Password)))
            {
                argon2.Salt = Encoding.UTF8.GetBytes("C3P123");
                argon2.DegreeOfParallelism = 2;
                argon2.MemorySize = 65536;
                argon2.Iterations = 4;

                var userAccount = new UserAccount
                {
                    Name = AccountName,
                    Email = Email,
                    PasswordHash = BitConverter.ToString(
                        argon2.GetBytes(32)).Replace("-", "").ToLowerInvariant()
                };

                db.Users.Add(userAccount);

                if (db.SaveChanges() == 1)
                    Close(); // success, close window
                else
                    ErrorMessage = "The account creation has failed";

            }
        }
        else
        {
            // account name is already used
            ErrorMessage = $"{AccountName} is already used.";
            AccountName = String.Empty;
        }
    }
    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        // close window
        Close();
    }
}