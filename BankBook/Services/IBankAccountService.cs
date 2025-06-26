using BankBook.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankBook.Services
{
    public interface IBankAccountService
    {
        Task<IEnumerable<BankAccount>> GetBankAccountsListAsync();

        Task<bool> AddBankAccountAsync(BankAccount bankAccount);

        Task<bool> DeleteBankAccountAsync(int Id);

        Task<bool> UpdateBankAccountAsync(BankAccount bankAccount);
    }
}
