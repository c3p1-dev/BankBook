using BankBook.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankBook.Services
{
    public interface IBankAccountService
    {
        Task<int> AddBankAccountAsync(BankAccount bankAccount);
        Task<IEnumerable<BankAccount>> GetBankAccountsListAsync();
    }
}
