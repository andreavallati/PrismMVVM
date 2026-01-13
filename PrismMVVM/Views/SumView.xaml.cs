using PrismMVVM.ViewModels.Interfaces;
using System.Windows.Controls;

namespace PrismMVVM.Views
{
    /// <summary>
    /// Interaction logic for SumView.xaml
    /// </summary>
    public partial class SumView : UserControl
    {
        public SumView(ISumViewModel sumViewModel)
        {
            DataContext = sumViewModel;
            InitializeComponent();
        }
    }
}
