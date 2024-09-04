using CompanyManager.Base;
using CompanyManager.Interfaces;
using System.Windows;

namespace CompanyManager.Services
{
    public class WindowManager(WindowMapper windowMapper) : IWindowManager
    {
        private Window? previousWindow { get; set; }

        public void ShowWindow(BaseViewModel viewModel, bool closePrevious = false)
        {
            var windowType = windowMapper.GetWindowTypeForViewModel(viewModel.GetType());
            if (windowType != null)
            {
                var window = Activator.CreateInstance(windowType) as Window;
                window.DataContext = viewModel;
                window.Show();

                if (closePrevious)
                {
                    previousWindow?.Close();
                }

                previousWindow = window;
            }
        }

        public void ClosePreviousWindow()
        {
            previousWindow?.Close();
            previousWindow = null;
        }
    }
}
