using CompanyManager.Base;
using CompanyManager.Interfaces;
using CompanyManager.Services;

namespace CompanyManager.ViewModel
{
    public class StartWindowVM : BaseViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly ViewModelLocator _viewLocator;

        public IItemsService IItemsService { get; private set; }
        public RelayCommand NavigateEmployeesCommand { get; set; }

        public StartWindowVM(IItemsService itemsService, IWindowManager windowManager, ViewModelLocator viewModelLocator)
        {
            _windowManager = windowManager;
            _viewLocator = viewModelLocator;
            IItemsService = itemsService;

            IItemsService.SetData<EmployeeWindowVM>(1);
            NavigateEmployeesCommand = new RelayCommand(o => { _windowManager.ShowWindow(_viewLocator.EmployeeWindowVM); }, o => true);
        }

    }
}
