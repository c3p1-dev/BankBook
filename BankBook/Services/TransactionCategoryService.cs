using BankBook.Data;
using BankBook.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankBook.Services
{
    public class TransactionCategoryService : ITransactionCategoryService
    {
        private readonly BankBookContext _dbContext;
        public TransactionCategoryService(BankBookContext dbContext) => _dbContext = dbContext;

        public Task<bool> AddTransactionCategoryAsync(TransactionCategory transactionCategory)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddTransactionSubCategoryAsync(TransactionCategory transactionSubCategory)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TransactionCategory>> GetTransactionCategoriesAsync()
        {
            var result = await _dbContext.TransactionCategories.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TransactionSubCategory>> GetTransactionSubCategoriesAsync(string CategoryCode)
        {
            var result = await _dbContext.TransactionSubCategories.Where(x => x.Category == CategoryCode).ToListAsync();

            return result;
        }
    }
}
