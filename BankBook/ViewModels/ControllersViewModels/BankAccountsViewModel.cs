using BankBook.Data.Models;
using BankBook.Services;
using System.Collections.ObjectModel;

namespace BankBook.ViewModels.ControllersViewModels
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
            var entities = await _bankAccountService.GetBankAccountsListAsync();
            foreach (var entity in entities)
            {
                var vm = new BankAccountViewModel()
                {
                    Id = entity.Id,
                    Bank = entity.Bank,
                    Name = entity.Name,
                    Balance = entity.Balance,
                    Swift = entity.Swift,
                    IBAN = entity.IBAN,
                    Description = entity.Description,
                    Url = entity.Url
                };
                Accounts.Add(vm);
            }
        }

        public BankAccountViewModel NewAccount { get; } = new();

        /*public void AddAccount()
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
        }*/

        public async void DeleteAccountAsync()
        {
            if (SelectedAccount != null)
            {
                // delete the account
                await _bankAccountService.DeleteBankAccountAsync(SelectedAccount.Id);
                SelectedAccount = null;

                // reload the list
                LoadAccounts();
            }
        }

        public async void UpdateAccountAsync()
        {
            if (SelectedAccount != null)
            {
                // create the model
                BankAccount model = new BankAccount
                {
                    Id = SelectedAccount.Id,
                    Bank = SelectedAccount.Bank,
                    Name = SelectedAccount.Name,
                    Balance = SelectedAccount.Balance,
                    Swift = SelectedAccount.Swift,
                    IBAN = SelectedAccount.IBAN,
                    Description = SelectedAccount.Description,
                    Url = SelectedAccount.Url,
                };
                // update the account
                await _bankAccountService.UpdateBankAccountAsync(model);

                // reload the list
                LoadAccounts();
            }
        }
    }
}
