using CompanyManager.Base;
using CompanyManager.Interfaces;
using CompanyManager.Services;

namespace CompanyManager.ViewModel
{
    public class StartWindowVM : BaseViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly ViewModelLocator _viewLocator;
        public RelayCommand NavigateEmployeesCommand { get; set; }

        public StartWindowVM(IWindowManager windowManager, ViewModelLocator viewModelLocator)
        {
            _windowManager = windowManager;
            _viewLocator = viewModelLocator;

            NavigateEmployeesCommand = new RelayCommand(o => { _windowManager.ShowWindow(_viewLocator.EmployeeWindowVM); }, o => true);
        }
    }
}
