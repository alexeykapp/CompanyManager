using CompanyManager.Base;
using CompanyManager.Converters;
using CompanyManager.Database;
using CompanyManager.DataBase.DisplayModel;
using CompanyManager.Interfaces;
using CompanyManager.Repositories;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CompanyManager.ViewModel
{
    public class EditEmployeeVM : BaseViewModel
    {
        private readonly IViewModelDataService viewModelData;
        private readonly ByteImage byteImage;
        private readonly RoleRepository roleRepository;
        private readonly EmployeeRepository employeeRepository;
        private readonly PhotoRepository photoRepository;
        private readonly EmployeeDisplayModelConverter employeeConverter;
        private EmployeeDisplayModel employee;
        private List<Role> roles;

        public EditEmployeeVM(IViewModelDataService viewModelData, ByteImage byteImage, RoleRepository roleRepository, EmployeeRepository employeeRepository, PhotoRepository photoRepository, EmployeeDisplayModelConverter employeeConverter)
        {
            this.viewModelData = viewModelData;
            this.byteImage = byteImage;
            this.roleRepository = roleRepository;
            this.employeeRepository = employeeRepository;
            this.photoRepository = photoRepository;
            this.employeeConverter = employeeConverter;
            UploadPhotoCommand = new AsyncRelayCommand(_ => UploadPhotoAsync());
            LoadDataCommand = new AsyncRelayCommand(_ => LoadDataAsync());
            SaveChangesCommand = new AsyncRelayCommand(_ => SaveChangesAsync());
            employee = this.viewModelData.GetData<EditEmployeeVM, EmployeeDisplayModel>();
            this.viewModelData.ClearData<EditEmployeeVM>();
            if (employee == null)
            {
                employee = new EmployeeDisplayModel
                {
                    Roles = new List<Role>()
                };
            }
        }
        #region COMMANDS
        public AsyncRelayCommand UploadPhotoCommand { get; set; }
        public AsyncRelayCommand SaveChangesCommand { get; set; }
        public AsyncRelayCommand LoadDataCommand { get; set; }
        #endregion
        #region PROPERTIES
        public string FirstName
        {
            get => employee.FirstName!;
            set
            {
                if (employee.FirstName != value)
                {
                    employee.FirstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        public string LastName
        {
            get => employee.LastName!;
            set
            {
                if (employee.LastName != value)
                {
                    employee.LastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        public string MiddleName
        {
            get => employee.MiddleName!;
            set
            {
                if (employee.MiddleName != value)
                {
                    employee.MiddleName = value;
                    OnPropertyChanged(nameof(MiddleName));
                }
            }
        }

        public DateTime? DateOfBirth
        {
            get
            {
                if (employee.DateOfBirth == null)
                {
                    return DateTime.Today;
                }
                return employee.DateOfBirth!.Value.ToDateTime(TimeOnly.MinValue);
            }
            set
            {
                employee.DateOfBirth = DateOnly.FromDateTime((DateTime)value!);
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }

        public string Phone
        {
            get => employee.Phone!;
            set
            {
                if (employee.Phone != value)
                {
                    employee.Phone = value;
                    OnPropertyChanged(nameof(Phone));
                }
            }
        }

        public string Passport
        {
            get => employee.Passport!;
            set
            {
                if (employee.Passport != value)
                {
                    employee.Passport = value;
                    OnPropertyChanged(nameof(Passport));
                }
            }
        }

        public string Address
        {
            get => employee.Address!;
            set
            {
                if (employee.Address != value)
                {
                    employee.Address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        public Role SelectedRole
        {
            get
            {
                if (employee.Roles!.Count == 0)
                {
                    return null!;
                }
                return employee.Roles![0];
            }
            set
            {
                if (employee.Roles!.Count == 0)
                {
                    employee.Roles.Add(value);
                }
                else
                {
                    employee.Roles![0] = value;
                }
                OnPropertyChanged(nameof(SelectedRole));
            }
        }
        public ImageSource Photo
        {
            get
            {
                if (employee.Photo == null)
                {
                    var resourceUri = new Uri("pack://application:,,,/CompanyManager;component/Resources/NoPhoto.jpeg", UriKind.Absolute);
                    return new BitmapImage(resourceUri);
                }
                return byteImage.ConvertByteArrayToBitmapImage(employee.Photo!);
            }
        }
        public List<Role> Roles
        {
            get => roles;
            set
            {
                roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }
        public string WindowTitle => employee.IdEmployee == 0 ? "Добавить сотрудника" : "Редактировать сотрудника";

        #endregion
        private async Task UploadPhotoAsync()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var photoBytes = File.ReadAllBytes(openFileDialog.FileName);
                employee.Photo = photoBytes;
                //await photoRepository.UpdatePhotoAsync(employee.IdEmployee, photoBytes);
                OnPropertyChanged(nameof(Photo));
            }
        }
        private async Task LoadDataAsync()
        {
            Roles = await roleRepository.GetAsync();
            if (employee.Roles != null)
                SelectedRole = Roles.FirstOrDefault(r => r.IdRole == employee.Roles![0].IdRole)!;
            var employeePhoto = await photoRepository.GetPhotoEmployeeAsync(employee!.IdEmployee);
            if (employeePhoto != null)
            {
                employee.Photo = employeePhoto!.PhotoEmployee1;
                OnPropertyChanged(nameof(Photo));
            }
        }
        private async Task SaveChangesAsync()
        {
            if (employee.IdEmployee == 0)
            {
                var employeeNew = employeeConverter.ConvertToEmployee(employee);
                await employeeRepository.AddAsync(employeeNew);
                await photoRepository.UpdatePhotoAsync(employeeNew.IdEmployee, employee.Photo!);
                await roleRepository.AddRoleToEmployee(Roles, employeeNew);
            }
            else
            {
                await employeeRepository.UpdateEmployeeAsync(employee);
            }
        }
        private bool Validate()
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

            if (!DateOfBirth.HasValue)
            {
                MessageBox.Show("Дата рождения обязательна.");
                return false;
            }
            if (employee.Roles!.Count > 0)
            {
                MessageBox.Show("Должность обязательна.");
                return false;
            }
            return true;
        }
    }
}
