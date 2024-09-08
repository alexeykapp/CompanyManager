using CompanyManager.Base;

namespace CompanyManager.Interfaces
{
    public interface IWindowManager
    {
        void ShowWindow(BaseViewModel viewModel, bool closePrevious = false);
        void CloseCurrentWindow();
    }
}
