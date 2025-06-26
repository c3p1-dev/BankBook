using BankBook.Data;
using BankBook.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankBook.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly BankBookContext _dbContext;
        public BankAccountService() => _dbContext = new BankBookContext();

        public async Task<IEnumerable<BankAccount>> GetBankAccountsListAsync()
        {
            var result = await _dbContext.BankAccounts.ToListAsync();

            return result;
        }

        public async Task<bool> AddBankAccountAsync(BankAccount bankAccount)
        {
            // add bankAccount
            _dbContext.BankAccounts.Add(bankAccount);
            int result = await _dbContext.SaveChangesAsync();

            return result == 1;
        }

        public async Task<bool> DeleteBankAccountAsync(int Id)
        {
            var account = await _dbContext.BankAccounts.Where(x => x.Id == Id).FirstAsync();
            _dbContext.BankAccounts.Remove(account);
            int result = await _dbContext.SaveChangesAsync();

            return result == 1;
        }

        public async Task<bool> UpdateBankAccountAsync(BankAccount bankAccount)
        {
            var account = await _dbContext.BankAccounts.Where(x => x.Id == bankAccount.Id).FirstAsync();
            account.Name = bankAccount.Name;
            account.Bank = bankAccount.Bank;
            account.Balance = bankAccount.Balance;
            account.Name = bankAccount.Name;
            account.Description = bankAccount.Description;
            account.Swift = bankAccount.Swift;
            account.IBAN = bankAccount.IBAN;
            account.Url = bankAccount.Url;

            if (account.LockedAt is not null)
                account.LockedAt = bankAccount.LockedAt;

            _dbContext.BankAccounts.Update(account);
            int result = await _dbContext.SaveChangesAsync();

            return result == 1;
        }
    }
}
