using CompanyManager.Base;
using CompanyManager.Interfaces;
using System.ComponentModel;
using System.Windows;

namespace CompanyManager.Services
{
    public class WindowManager(WindowMapper windowMapper) : IWindowManager
    {
        private Stack<Window>? windows = new();
        public void ShowWindow(BaseViewModel viewModel, bool closeCurrent = false)
        {
            var windowType = windowMapper.GetWindowTypeForViewModel(viewModel.GetType());
            if (windowType != null)
            {
                var window = Activator.CreateInstance(windowType) as Window;
                window!.DataContext = viewModel;
                window.Closing += WindowClosing;

                if (closeCurrent)
                {
                    CloseCurrentWindow();
                }

                window.Show();
                if (windows!.Count >= 1)
                {
                    windows.First().IsEnabled = false;
                }
                windows!.Push(window);
            }
        }

        public void CloseCurrentWindow()
        {
            windows?.First().Close();
            windows?.Pop();
        }
        private void WindowClosing(object? sender, CancelEventArgs e)
        {
            if (windows!.Count > 1)
            {
                var window = sender as Window;
                var typeClosingWindow = window!.GetType();
                if (typeClosingWindow != windows.First().GetType())
                {
                    e.Cancel = true;
                }
                else
                {
                    windows.Pop();
                    windows.First().IsEnabled = true;
                }
            }
        }
    }
}
