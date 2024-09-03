using CompanyManager.Base;
using CompanyManager.Interfaces;
using System.Windows;

namespace CompanyManager.Services
{
    public class WindowManager(WindowMapper windowMapper) : IWindowManager
    {
        public void CloseWindow()
        {
            
        }

        public void ShowWindow(BaseViewModel viewModel)
        {
            var windowType = windowMapper.GetWindowTypeForViewModel(viewModel.GetType());
            if (windowType != null)
            {
                var window = Activator.CreateInstance(windowType) as Window;
                window.DataContext = viewModel;
                window.Show();
            }
        }
    }
}
