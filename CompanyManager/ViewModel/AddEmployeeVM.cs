using CompanyManager.Base;
using CompanyManager.Database;
using CompanyManager.Repositories;
using System.Text.RegularExpressions;
using System.Windows;

namespace CompanyManager.ViewModel
{
    public class AddEmployeeVM : BaseViewModel
    {
        private EmployeeRepository employeeRepository;
        private string? lastName;
        private string? firstName;
        private string? middleName;
        private string? passport;
        private string? phone;
        private string? address;
        public AddEmployeeVM(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            AddEmployeeCommand = new AsyncRelayCommand(_ => AddEmployeeAsync());
        }
        public AsyncRelayCommand AddEmployeeCommand { get; set; }
        #region PROPERTIES
        public string LastName
        {
            get => lastName;
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string FirstName
        {
            get => firstName;
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged();

                }
            }
        }
        public string MiddleName
        {
            get => middleName;
            set
            {
                if (middleName != value)
                {
                    middleName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Passport
        {
            get => passport;
            set
            {
                if (passport != value)
                {
                    passport = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Phone
        {
            get => phone;
            set
            {
                if (phone != value)
                {
                    phone = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Address
        {
            get => address;
            set
            {
                if (address != value)
                {
                    address = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
        private async Task AddEmployeeAsync()
        {
            if (!CanExecuteAddCommand())
                return;
            try
            {
                await employeeRepository.Add(GetEmployee());
                ClearProperties();
                MessageBox.Show("Успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ClearProperties();
        }
        private bool CanExecuteAddCommand()
        {
            if (string.IsNullOrWhiteSpace(LastName))
            {
                MessageBox.Show("Фамилия обязательна.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(FirstName))
            {
                MessageBox.Show("Имя обязательно.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(MiddleName))
            {
                MessageBox.Show("Отчество обязательно.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Passport) || !Regex.IsMatch(Passport, @"^\d{10}$"))
            {
                MessageBox.Show("Неверный формат паспорта. Используйте формат XXXX-XXXXXX (X - цифра).");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Phone) || !Regex.IsMatch(Phone, @"^\+?\d{10,15}$"))
            {
                MessageBox.Show("Неверный формат телефона. Используйте только цифры и знак '+' в начале.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Address))
            {
                MessageBox.Show("Адрес обязателен.");
                return false;
            }
            return true;
        }
        private Employee GetEmployee()
        {
            Employee employee = new()
            {
                Address = Address,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                Passport = Passport,
                Phone = Phone,
            };
            return employee;
        }
        private void ClearProperties()
        {
            Address = string.Empty;
            FirstName = string.Empty;
            MiddleName = string.Empty;
            LastName = string.Empty;
            Passport = string.Empty;
            Phone = string.Empty;
        }
    }
}
