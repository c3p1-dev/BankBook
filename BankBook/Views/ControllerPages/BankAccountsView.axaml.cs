using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
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
    }
}