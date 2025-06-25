using BankBook.Data;
using BankBook.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBook.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly BankBookContext _dbContext;
        public BankAccountService() => _dbContext = new BankBookContext();
        public async Task<int> AddBankAccountAsync(BankAccount bankAccount)
        {
            // add bankAccount
            _dbContext.BankAccounts.Add(bankAccount);
            int result = await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<BankAccount>> GetBankAccountsListAsync()
        {
            var result = await _dbContext.BankAccounts.ToListAsync();

            return result;
        }
    }
}
