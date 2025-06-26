using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using BankBook.ViewModels;

namespace BankBook.Views.ControllerPages
{
    public partial class BankAccountsView : UserControl
    {
        public BankAccountsView()
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

        private void OnDeleteClicked(object sender, RoutedEventArgs e)
        {
            if (DataContext is BankAccountsViewModel vm)
            {
                vm.DeleteAccountAsync();
            }
        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            if (DataContext is BankAccountsViewModel vm)
            {
                vm.UpdateAccountAsync();
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