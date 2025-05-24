using BankBook.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BankBook.Data
{
   internal class BankBookContext : DbContext
    {
        public DbSet<UserAccount> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=BankBook.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
