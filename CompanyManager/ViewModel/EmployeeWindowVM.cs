using CompanyManager.Base;
using CompanyManager.DataBase.DisplayModel;
using CompanyManager.Interfaces;
using CompanyManager.Repositories;
using CompanyManager.Services;

namespace CompanyManager.ViewModel
{
    public class EmployeeWindowVM : BaseViewModel
    {
        private EmployeeRepository employeeRepository;
        private IWindowManager windowManager;
        private ViewModelLocator viewModelLocator;
        private IItemsService itemsService;
        private List<EmployeeDisplayModel> employees;
        public List<EmployeeDisplayModel> Employees
        {
            get => employees;
            set
            {
                employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }
        public AsyncRelayCommand LoadDataCommand { get; set; }
        public RelayCommand NavigateAddEmployeeCommand { get; set; }
        public RelayCommand NavigateEditCommand { get; set; }
        public EmployeeWindowVM(EmployeeRepository employeeRepository, IWindowManager windowManager, ViewModelLocator viewModelLocator, IItemsService itemsService)
        {
            this.employeeRepository = employeeRepository;
            this.windowManager = windowManager;
            this.viewModelLocator = viewModelLocator;
            this.itemsService = itemsService;
            LoadDataCommand = new AsyncRelayCommand(async _ => await LoadDataAsync());
            NavigateAddEmployeeCommand = new RelayCommand(o => { windowManager.ShowWindow(viewModelLocator.AddEmployeeVM); }, o => true);
            NavigateEditCommand = new RelayCommand(obj => NavigateEdit((EmployeeDisplayModel)obj));
        }

        private async Task LoadDataAsync()
        {
            Employees = await employeeRepository.GetWithRolesAsync();
        }
        private void NavigateEdit(EmployeeDisplayModel employeeDisplayModel)
        {
            itemsService.SetData<EditEmployeeVM>(employeeDisplayModel);
            windowManager.ShowWindow(viewModelLocator.EditEmployeeVM);
        }
    }
}
