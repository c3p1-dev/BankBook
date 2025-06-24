using Avalonia.Controls;
using BankBook.ViewModels;

namespace BankBook;

public partial class ConverterUsagePageView : UserControl
{
    ConverterUsageWithDataGridWindowViewModel _viewModel = new();
    public ConverterUsagePageView()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }
}