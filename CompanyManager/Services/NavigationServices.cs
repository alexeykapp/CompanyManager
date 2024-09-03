using CompanyManager.Base;
using CompanyManager.Interfaces;

namespace CompanyManager.Services
{
    public class NavigationServices : BaseViewModel, INavigationService
    {
        private readonly Func<Type, BaseViewModel> _viewModelFactory;

        private BaseViewModel? _currentView;

        public BaseViewModel CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public NavigationServices(Func<Type, BaseViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
        {
            var viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }
    }
}
