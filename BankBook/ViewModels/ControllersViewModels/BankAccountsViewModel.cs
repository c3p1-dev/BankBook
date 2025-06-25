using BankBook.Data;
using BankBook.Services;
using System.Collections.ObjectModel;

namespace BankBook.ViewModels
{
    public class BankAccountsViewModel : ViewModelBase
    {
        private readonly IBankAccountService _bankAccountService;
        public BankAccountsViewModel()
        {
            _bankAccountService = new BankAccountService();

            // Load the account list
            LoadAccounts();
        }
        public ObservableCollection<BankAccountViewModel> Accounts { get; } = new();

        private BankAccountViewModel? _selectedAccount;
        public BankAccountViewModel? SelectedAccount
        {
            get => _selectedAccount;
            set => RaiseAndSetIfChanged(ref _selectedAccount, value);
        }

        public async void LoadAccounts()
        {
            Accounts.Clear();
            var entities = await _bankAccountService.GetBankAccountsListAsync(); // méthode à créer dans service
            foreach (var entity in entities)
            {
                var vm = new BankAccountViewModel()
                {
                    Bank = entity.Bank,
                    Name = entity.Name,
                    Swift = entity.Swift,
                    IBAN = entity.IBAN,
                    Description = entity.Description,
                    Url = entity.Url
                };
                Accounts.Add(vm);
            }
        }

        public BankAccountViewModel NewAccount { get; } = new();

        public void AddAccount()
        {
            var newAcc = new BankAccountViewModel()
            {
                Bank = NewAccount.Bank,
                Name = NewAccount.Name,
                Swift = NewAccount.Swift,
                IBAN = NewAccount.IBAN,
                Description = NewAccount.Description,
                Url = NewAccount.Url                
            };

            Accounts.Add(newAcc);
            SelectedAccount = newAcc;

            RaisePropertyChanged(nameof(NewAccount));
        }
    }
}
