using Avalonia.Data.Converters;
using BankBook.Data;
using BankBook.Data.Models;
using BankBook.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BankBook.ViewModels.ControllersViewModels
{
    public class BankAccountViewModel : ViewModelBase
    {
        private IBankAccountService _bankAccountService;
        public BankAccountViewModel()
        {
            // init services for the ViewModel
            BankBookContext db = new BankBookContext();
            _bankAccountService = new BankAccountService(db);
        }
        public Guid Id { get; set; }

        private string _bank = "My Bank";
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
        [StringLength(11, ErrorMessage = "SWIFT/BIC too long (max 11 characters)")]
        [RegularExpression("^[A-Z]{4}[A-Z]{2}[A-Z0-9]{2}([A-Z0-9]{3})?$", ErrorMessage = "Invalid SWIFT/BIC format")]
        public string Swift { get => _swift; set => RaiseAndSetIfChanged(ref _swift, value); }

        public string _iban = "FR7630006000011234567890189";
        [StringLength(34, ErrorMessage = "IBAN too long (max 34 characters)")]
        [RegularExpression("^[A-Z]{2}[0-9]{2}[A-Z0-9]{11,30}$", ErrorMessage = "Invalid IBAN format")]
        public string IBAN { get => _iban; set => RaiseAndSetIfChanged(ref _iban, value); }

        private decimal _balance = decimal.Zero;
        public decimal Balance { get => _balance; set => RaiseAndSetIfChanged(ref _balance, value); }

        [Required(ErrorMessage = "Account Code must be defined")]
        [StringLength(10, ErrorMessage = "Account Code is too long (max 10 characters)")]
        public string Code { get => _code; set => RaiseAndSetIfChanged(ref _code, value); }
        private string _code = "MYAC";

        public string _url = string.Empty;
        [StringLength(2083, ErrorMessage = "URL too long (max 2083 characters)")]
        public string Url { get => _url; set => RaiseAndSetIfChanged(ref _url, value); }

        public string RIB { get => IbanToRib(_iban)!; }
        public string AccountNumber { get => IbanToAccountNumber(_iban)!; }

        private static string? IbanToRib(string iban)
        {
            // Nettoyer l'IBAN (supprimer les espaces)
            iban = iban.Replace(" ", "").ToUpper();

            // Vérifier qu'on a bien un IBAN français de 27 caractères
            if (!iban.StartsWith("FR") || iban.Length != 27)
                return null;

            string bankCode = iban.Substring(4, 5);
            string officeCode = iban.Substring(9, 5);
            string accountNumber = iban.Substring(14, 11);
            string ribKey = iban.Substring(25, 2);

            return $"{bankCode} {officeCode} {accountNumber} {ribKey}";
        }
        private static string? IbanToAccountNumber(string iban)
        {
            // Nettoyer l'IBAN (supprimer les espaces)
            iban = iban.Replace(" ", "").ToUpper();

            // Vérifier qu'on a bien un IBAN français de 27 caractères
            if (!iban.StartsWith("FR") || iban.Length != 27)
                return null;

            string bankCode = iban.Substring(4, 5);
            string officeCode = iban.Substring(9, 5);
            string accountNumber = iban.Substring(14, 11);
            string ribKey = iban.Substring(25, 2);

            return accountNumber;
        }

        public override string ToString() => $"{Code} - {Name} ({Bank}) - {Balance.ToString("C2")}";


        public async Task AddAccountAsync(BankAccount account)
        {
            await _bankAccountService.AddBankAccountAsync(account);
        }

        public async Task<bool> IsAccountCodeUniqueAsync(string accountCode)
        {
            return await _bankAccountService.IsAccountCodeUniqueAsync(accountCode);
        }
    }
}