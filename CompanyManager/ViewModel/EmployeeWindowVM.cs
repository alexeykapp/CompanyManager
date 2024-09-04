using CompanyManager.Base;
using CompanyManager.Database;
using CompanyManager.Interfaces;
using CompanyManager.Repositories;
using CompanyManager.Services;

namespace CompanyManager.ViewModel
{
    public class EmployeeWindowVM : BaseViewModel
    {
        private EmployeeRepository employeeRepository;
        private IWindowManager windowManager;
        private List<Employee> employees;
        public List<Employee> Employees
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
        public EmployeeWindowVM(EmployeeRepository employeeRepository, IWindowManager windowManager, ViewModelLocator viewModelLocator)
        {
            this.employeeRepository = employeeRepository;
            this.windowManager = windowManager;
            LoadDataCommand = new AsyncRelayCommand(async _ => await LoadDataAsync());
            NavigateAddEmployeeCommand = new RelayCommand(o => { windowManager.ShowWindow(viewModelLocator.AddEmployeeVM); }, o => true);
        }

        private async Task LoadDataAsync()
        {
            Employees = await employeeRepository.GetAsync();
        }
    }
}
