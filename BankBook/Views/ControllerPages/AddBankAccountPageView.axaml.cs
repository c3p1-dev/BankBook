using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia.VisualTree;
using BankBook.Data.Models;
using BankBook.Utils;
using BankBook.ViewModels;
using BankBook.ViewModels.ControllersViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BankBook.Views.ControllerPages
{
    public partial class AddBankAccountPageView : UserControl
    {
        public AddBankAccountPageView()
        {
            InitializeComponent();

            DataContext = new BankAccountViewModel();

        }

        private void OnCancelClicked(object sender, RoutedEventArgs e)
        {
            ClearFields();

            // Go back to BankAccountsPage
            MainWindowViewModel.InstanceMainWindowVM!.NavigateToPage(Models.PagesEnum.BankAccountsPage);
        }

        private async void OnAddClicked(object sender, RoutedEventArgs e)
        {
            if (DataContext is BankAccountViewModel vm)
            {
                // TODO : validate data and save
                var context = new ValidationContext(vm, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();
                // probleme ici
                bool isValid = Validator.TryValidateObject(vm, context, results, validateAllProperties: true);

                if (!isValid)
                {
                    // Not Valid
                }
                else
                {
                    // check if account code is unique
                    if (await vm.IsAccountCodeUniqueAsync(vm.Code))
                    {
                        // Add the account to the database
                        BankAccount account = new BankAccount
                        {
                            Bank = vm.Bank,
                            Name = vm.Name,
                            Code = vm.Code,
                            Description = vm.Description,
                            IBAN = vm.IBAN,
                            Swift = vm.Swift,
                            Url = vm.Url,
                        };
                        await vm.AddAccountAsync(account);

                        var owner = VisualRoot as Window;
                        if (owner is null)
                        {
                            return;
                        }
                        await TaskDialogHelper.ShowTaskDialogAsync(owner, "Success", $"The Account {vm.Code} - {vm.Name} has been created", TaskDialogType.Success);
                        // Clear fields and go back to BankAccountsPage
                        ClearFields();
                        MainWindowViewModel.InstanceMainWindowVM!.NavigateToPage(Models.PagesEnum.BankAccountsPage);
                    }
                    else // account code already exists
                    {
                        // Faut mettre quoi ici pour ajouter un message d'erreur de validation qui dit que le code compte est pas unique ?
                        var owner = VisualRoot as Window;
                        if (owner is null)
                        {
                            return;
                        }

                        // Show an error dialog
                        await TaskDialogHelper.ShowTaskDialogAsync(owner, "Error", $"The Account Code {vm.Code} already exists", TaskDialogType.Error);
                    }

                }
            }
        }
        private void TextBox_GotFocus(object? sender, GotFocusEventArgs e)
        {
            if (sender is TextBox tb)
            {
                Dispatcher.UIThread.Post(() => tb.SelectAll());
            }
        }
        private void TextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (sender is Control currentControl)
            {
                if (e.Key == Key.Enter)
                {
                    // Récupérer la fenêtre parente
                    var window = currentControl.GetVisualRoot() as Window;
                    if (window == null)
                        return;

                    // Trouver tous les contrôles focusables dans la fenêtre, triés par TabIndex
                    var focusableControls = window.GetVisualDescendants()
                        .OfType<Control>()
                        .Where(c => c.Focusable && c.IsEnabled && c.IsVisible)
                        .OrderBy(c => c.TabIndex)
                        .ToList();

                    // Trouver la position du contrôle courant
                    int index = focusableControls.IndexOf(currentControl);

                    if (index == -1)
                        return;

                    if (index == 15) // Url field, Enter trig OnAddClicked
                        OnAddClicked(sender, e);

                    // Aller au suivant (boucle)
                    int nextIndex = (index + 1) % focusableControls.Count;
                    var nextControl = focusableControls[nextIndex];

                    // Mettre le focus sur le contrôle suivant
                    nextControl.Focus();

                    e.Handled = true;
                }
                else if (e.Key == Key.Escape)
                {
                    OnCancelClicked(sender, e);
                }
            }
        }
        private void ClearFields()
        {
            // Clear fields
            if (DataContext is BankAccountViewModel vm)
            {
                vm.Bank = "My Bank";
                vm.Name = "My Account";
                vm.Code = "MYAC";
                vm.Description = string.Empty;
                vm.Url = string.Empty;
                vm.IBAN = "FR7630006000011234567890189";
                vm.Swift = "BNPAFRPPXXX";
            }
        }
    }
}
