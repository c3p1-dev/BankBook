using Avalonia.Controls;
using BankBook.Models;
using BankBook.ViewModels;

namespace BankBook;

public partial class HomePageView : UserControl
{
    private HomePageViewModel _viewModel = new();
    public HomePageView()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }

    private void Page_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var selectedPage = (sender as Button)?.DataContext as PageModel;

        if (selectedPage is null)
        {
            return;
        }

        MainWindowViewModel.InstanceMainWindowVM.NavigateToPage(selectedPage.Page);

    }
}