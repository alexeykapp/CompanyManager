using CompanyManager.ViewModel;
using System.Windows;

namespace CompanyManager.View
{
    public partial class StartWindow : Window
    {
        public StartWindow(StartWindowVM startWindowVM)
        {
            InitializeComponent();
            DataContext = startWindowVM;
        }
    }
}
