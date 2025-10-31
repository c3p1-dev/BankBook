using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using BankBook.Utils;
using BankBook.ViewModels;
using BankBook.ViewModels.ControllersViewModels;

namespace BankBook.Views.ControllerPages
{
    public partial class BankAccountsPageView : UserControl
    {
        public BankAccountsPageView()
        {
            InitializeComponent();

            this.AttachedToVisualTree += OnLoaded;
        }

        public void OnLoaded(object? sender, VisualTreeAttachmentEventArgs e)
        {
            if (DataContext is BankAccountsViewModel vm)
            {
                vm.LoadAccounts();
            }
        }

        private void OnAddAccountClicked(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.InstanceMainWindowVM!.NavigateToPage(Models.PagesEnum.AddBankAccountPage);

        }

        private async void OnDeleteClicked(object sender, RoutedEventArgs e)
        {
            if (DataContext is BankAccountsViewModel vm && vm.SelectedAccount is not null)
            {
                var owner = VisualRoot as Window;
                if (owner is null)
                {
                    return;
                }
                // Show a question dialog
                var result = await TaskDialogHelper.ShowQuestionDialogAsync(owner, "Question", $"Are you sure you want to delete {vm.SelectedAccount?.Code} - {vm.SelectedAccount?.Name} ");

                if (result) // user clicked yes
                {
                    vm.DeleteAccountAsync();
                }
            }
        }

        private async void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            if (DataContext is BankAccountsViewModel vm)
            {
                // check if Account Code is unique
                if (vm.IsSelectedAccountCodeUniqueAsync().Result)
                {
                    var owner = VisualRoot as Window;
                    if (owner is null)
                    {
                        return;
                    }
                    await TaskDialogHelper.ShowTaskDialogAsync(owner, "Success", $"The Account {vm.SelectedAccount?.Code} - {vm.SelectedAccount?.Name} has been updated", TaskDialogType.Success);
                    vm.UpdateAccountAsync();
                }
                else
                {
                    var owner = VisualRoot as Window;
                    if (owner is null)
                    {
                        return;
                    }
                    await TaskDialogHelper.ShowTaskDialogAsync(owner, "Error", $"The Account Code {vm.SelectedAccount?.Code} already exists ", TaskDialogType.Error);
                    vm.LoadAccounts();
                }
            }
        }

        private void OnRefreshClicked(object sender, RoutedEventArgs e)
        {
            if (DataContext is BankAccountsViewModel vm)
            {
                vm.LoadAccounts();
            }
        }

        private void TextBox_GotFocus(object? sender, GotFocusEventArgs e)
        {
            if (sender is TextBox tb)
            {
                Dispatcher.UIThread.Post(() => tb.SelectAll());
            }
        }
    }
}