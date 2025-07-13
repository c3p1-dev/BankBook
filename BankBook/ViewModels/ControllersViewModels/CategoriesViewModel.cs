using BankBook.Data.Models;
using System.Collections.ObjectModel;

namespace BankBook.ViewModels.ControllersViewModels
{
    public class CategoriesViewModel : ViewModelBase
    {
        public ObservableCollection<TransactionCategory>? TransactionCategories { get; set; }
        public ObservableCollection<TransactionSubCategory>? TransactionSubCategories { get; set; }
    }
}
