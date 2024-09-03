using CompanyManager.Base;
using CompanyManager.Interfaces;

namespace CompanyManager.ViewModel
{
    public class StartWindowVM : BaseViewModel
    {
        public RelayCommand NavigateEmployeesCommand { get; set; }
        private INavigationService navigationService;
        public INavigationService NavigationService
        {
            get => navigationService;
            set
            {
                navigationService = value;
                OnPropertyChanged();
            }
        }
        public StartWindowVM(INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigateEmployeesCommand = new RelayCommand(o => { NavigationService.NavigateTo<EmployeeWindowVM>(); }, o => true);
        }

    }
}
