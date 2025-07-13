using BankBook.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankBook.Services
{
    public interface ITransactionCategoryService
    {
        Task<IEnumerable<TransactionCategory>> GetTransactionCategoriesAsync();
        Task<bool> AddTransactionCategoryAsync(TransactionCategory transactionCategory);


        Task<IEnumerable<TransactionSubCategory>> GetTransactionSubCategoriesAsync(string CategoryCode);
        Task<bool> AddTransactionSubCategoryAsync(TransactionCategory transactionSubCategory);
    }
}
