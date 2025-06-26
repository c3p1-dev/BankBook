using BankBook.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace BankBook.Data
{
    public class BankBookContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO : add the connection string to a configuration file
            var dbPath = Path.Combine(AppContext.BaseDirectory, "bankbook.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
 //           optionsBuilder.UseSqlite("Data Source=BankBook.db");

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

        public static void InitDatabase()
        {
            var context = new BankBookContext();

            try
            {
                // do the migration
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                // TODO : implement a logger system
                Console.WriteLine(ex.ToString());
            }

        }
    }
}