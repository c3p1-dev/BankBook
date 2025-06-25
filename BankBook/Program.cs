using Avalonia;
using Avalonia.ReactiveUI;
using BankBook.Data;
using BankBook.Data.Models;
using System;

namespace BankBook
{
    internal sealed class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.

        /*
         * [STAThread]
         * public static void Main(string[] args) => BuildAvaloniaApp()
         *  .StartWithClassicDesktopLifetime(args);
        */
        [STAThread]


        public static void Main(string[] args)
        {
            // Initialize database
            BankBookContext.InitDatabase();

            // Start application
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace()
                .UseReactiveUI();
    }
}
