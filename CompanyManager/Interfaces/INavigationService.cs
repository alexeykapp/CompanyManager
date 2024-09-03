using CompanyManager.Base;

namespace CompanyManager.Interfaces
{
    public interface INavigationService
    {
        BaseViewModel CurrentView { get; }
        void NavigateTo<T>() where T : BaseViewModel;
    }
}
