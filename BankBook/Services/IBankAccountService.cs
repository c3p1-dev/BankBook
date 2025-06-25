using BankBook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBook.Services
{
    public interface IBankAccountService
    {
        Task<int> AddBankAccountAsync(BankAccount bankAccount);
        Task<IEnumerable<BankAccount>> GetBankAccountsListAsync();
    }
}
