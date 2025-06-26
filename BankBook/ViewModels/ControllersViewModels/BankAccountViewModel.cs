using BankBook.Data.Models;
using BankBook.Services;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BankBook.ViewModels
{
    public class BankAccountViewModel : ViewModelBase
    {
        private IBankAccountService _bankAccountService;
        public BankAccountViewModel()
        {
            _bankAccountService = new BankAccountService();
        }
        public int Id { get; set; }

        private string _bank = "My Bank";
        [Required(ErrorMessage = "Bank name must be defined")]
        [StringLength(150, ErrorMessage = "Bank name too long (max 150 characters)")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ][A-Za-zÀ-ÿ0-9 .'\-]{1,149}$", ErrorMessage = "Invalid bank name format")]
        public string Bank { get => _bank; set => RaiseAndSetIfChanged(ref _bank, value); }


        private string _name = "My Account";
        [Required(ErrorMessage = "Account Name must be defined")]
        [StringLength(100, ErrorMessage = "Account name too long (max 100 characters)")]
        public string Name { get => _name; set => RaiseAndSetIfChanged(ref _name, value); }

        private string _description = string.Empty;
        [StringLength(255, ErrorMessage = "Description too long (max 255 characters)")]
        public string Description { get => _description; set => RaiseAndSetIfChanged(ref _description, value); }

        private string _swift = "BNPAFRPPXXX";
        [Required(ErrorMessage = "SWIFT/BIC must be defined")]
        [StringLength(11, ErrorMessage = "SWIFT/BIC too long (max 11 characters)")]
        [RegularExpression("^[A-Z]{4}[A-Z]{2}[A-Z0-9]{2}([A-Z0-9]{3})?$", ErrorMessage = "Invalid SWIFT/BIC format")]
        public string Swift { get => _swift; set => RaiseAndSetIfChanged(ref _swift, value); }

        public string _iban = "FR7630006000011234567890189";
        [Required(ErrorMessage = "IBAN must be defined")]
        [StringLength(34, ErrorMessage = "IBAN too long (max 34 characters)")]
        [RegularExpression("^[A-Z]{2}[0-9]{2}[A-Z0-9]{11,30}$", ErrorMessage = "Invalid IBAN format")]
        public string IBAN { get => _iban; set => RaiseAndSetIfChanged(ref _iban, value); }

        public string _url = string.Empty;
        [StringLength(2083, ErrorMessage = "URL too long (max 2083 characters)")]
        public string Url { get => _url; set => RaiseAndSetIfChanged(ref _url, value); }

        public override string ToString() => $"{Name} ({Bank})";

        public async Task AddAccount(BankAccount account)
        {
            await _bankAccountService.AddBankAccountAsync(account);
        }
    }
}