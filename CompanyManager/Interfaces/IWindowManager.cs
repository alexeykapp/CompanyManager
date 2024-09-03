using CompanyManager.Base;

namespace CompanyManager.Interfaces
{
    public interface IWindowManager
    {
        void ShowWindow(BaseViewModel viewModel);
        void CloseWindow();
    }
}
