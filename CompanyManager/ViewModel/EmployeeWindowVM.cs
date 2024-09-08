﻿using CompanyManager.Base;
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
        private EmployeeFilter employeeFilter;
        private List<EmployeeDisplayModel> employees;
        private List<EmployeeDisplayModel> filteringEmployee;
        private string textSearch;
        public List<EmployeeDisplayModel> Employees
        {
            get => employees;
            set
            {
                employees = value;
                FilteringEmployees = employees;
                OnPropertyChanged(nameof(Employees));
            }
        }
        public List<EmployeeDisplayModel> FilteringEmployees
        {
            get => filteringEmployee;
            set
            {
                filteringEmployee = value;
                OnPropertyChanged(nameof(FilteringEmployees));
            }
        }
        public string TextSearch
        {
            get => textSearch;
            set
            {
                textSearch = value;
                OnPropertyChanged(nameof(TextSearch));
            }
        }
        public EmployeeWindowVM(EmployeeRepository employeeRepository, IWindowManager windowManager, ViewModelLocator viewModelLocator, IItemsService itemsService, EmployeeFilter employeeFilter)
        {
            this.employeeRepository = employeeRepository;
            this.windowManager = windowManager;
            this.viewModelLocator = viewModelLocator;
            this.itemsService = itemsService;
            this.employeeFilter = employeeFilter;

            LoadDataCommand = new AsyncRelayCommand(async _ => await LoadDataAsync());
            NavigateAddEmployeeCommand = new RelayCommand(o => { windowManager.ShowWindow(viewModelLocator.AddEmployeeVM); }, o => true);
            NavigateEditCommand = new RelayCommand(obj => NavigateEdit((EmployeeDisplayModel)obj));
            NavigateStartCommand = new RelayCommand(o => { windowManager.ShowWindow(viewModelLocator.StartWindowVM, true); }, o => true);
            FilteringEmployeesCommand = new RelayCommand(o => { FilteringEmployees = employeeFilter.FilterEmployees(filteringEmployee!, TextSearch)!; }, o => true);
            CancelFilteringCommand = new RelayCommand(o => { FilteringEmployees = Employees; TextSearch = ""; }, o => true);
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
        #region COMMANDS
        public AsyncRelayCommand LoadDataCommand { get; set; }
        public RelayCommand NavigateAddEmployeeCommand { get; set; }
        public RelayCommand NavigateEditCommand { get; set; }
        public RelayCommand NavigateStartCommand { get; set; }
        public RelayCommand FilteringEmployeesCommand { get; set; }
        public RelayCommand CancelFilteringCommand { get; set; }
        #endregion
    }
}
