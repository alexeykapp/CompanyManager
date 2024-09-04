using CompanyManager.Base;
using CompanyManager.Repositories;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace CompanyManager.ViewModel
{
    public class AddEmployeeVM : BaseViewModel, IDataErrorInfo
    {
        private EmployeeRepository employeeRepository;
        private string? _lastName;
        private string? _firstName;
        private string? _middleName;
        private string? _passport;
        private string? _phone;
        private string? _address;
        public AddEmployeeVM(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        #region PROPERTIES
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                    ValidateProperty(nameof(LastName), value);
                }
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged();
                    ValidateProperty(nameof(FirstName), value);
                }
            }
        }

        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (_middleName != value)
                {
                    _middleName = value;
                    OnPropertyChanged();
                    ValidateProperty(nameof(MiddleName), value);
                }
            }
        }

        public string Passport
        {
            get => _passport;
            set
            {
                if (_passport != value)
                {
                    _passport = value;
                    OnPropertyChanged();
                    ValidateProperty(nameof(Passport), value);
                }
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged();
                    ValidateProperty(nameof(Phone), value);
                }
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged();
                    ValidateProperty(nameof(Address), value);
                }
            }
        }
        #endregion

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(LastName):
                        return string.IsNullOrWhiteSpace(LastName) ? "Фамилия обязательна." : null!;
                    case nameof(FirstName):
                        return string.IsNullOrWhiteSpace(FirstName) ? "Имя обязательно." : null!;
                    case nameof(MiddleName):
                        return string.IsNullOrWhiteSpace(MiddleName) ? "Отчество обязательно." : null!;
                    case nameof(Passport):
                        return string.IsNullOrWhiteSpace(Passport) || !Regex.IsMatch(Passport, @"^\d{4}-\d{6}$") ? "Неверный формат паспорта." : null!;
                    case nameof(Phone):
                        return string.IsNullOrWhiteSpace(Phone) || !Regex.IsMatch(Phone, @"^\+?\d{10,15}$") ? "Неверный формат телефона." : null!;
                    case nameof(Address):
                        return string.IsNullOrWhiteSpace(Address) ? "Адрес обязателен." : null!;
                    default:
                        return null!;
                }
            }
        }

        public string Error => null!;

        private void ValidateProperty(string propertyName, object value)
        {
            OnPropertyChanged(propertyName);
        }
    }
}
