using Avalonia;
using BankBook.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace BankBook
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        //    => BuildAvaloniaApp()
        //    .StartWithClassicDesktopLifetime(args);
        {
            // Initialize database
            InitDatabase();

            // Start application
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();

        // Database initialization
        private static void InitDatabase()
        {
            // Init Sqlite database
            using var db = new BankBookContext();
            try
            {
                db.Database.Migrate();
            }
            catch (Exception ex)
            {
                // TODO : implement a logger system
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
