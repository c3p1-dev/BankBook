using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BankBook.Data
{
    internal class BankBookContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public BankBookContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO : add the connection string to a configuration file
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));

            base.OnConfiguring(optionsBuilder);
        }

        public static void InitDatabase()
        {
            // get connection string from json configuration file
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var context = new BankBookContext(config);

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