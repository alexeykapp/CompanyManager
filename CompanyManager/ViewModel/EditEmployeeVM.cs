using CompanyManager.Base;
using CompanyManager.Converters;
using CompanyManager.Database;
using CompanyManager.DataBase.DisplayModel;
using CompanyManager.Interfaces;
using CompanyManager.Repositories;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CompanyManager.ViewModel
{
    public class EditEmployeeVM : BaseViewModel
    {
        private EmployeeDisplayModel employee;
        private List<Role> roles;
        private IItemsService itemsService;
        private ByteImage byteImage;
        private RoleRepository roleRepository;
        private EmployeeRepository employeeRepository;
        private PhotoRepository photoRepository;

        public EditEmployeeVM(IItemsService itemsService, ByteImage byteImage, RoleRepository roleRepository, EmployeeRepository employeeRepository, PhotoRepository photoRepository)
        {
            this.itemsService = itemsService;
            this.byteImage = byteImage;
            this.roleRepository = roleRepository;
            this.employeeRepository = employeeRepository;
            this.photoRepository = photoRepository;
            UploadPhotoCommand = new AsyncRelayCommand(_ => UploadPhotoAsync());
            LoadDataCommand = new AsyncRelayCommand(_ => LoadDataAsync());
            SaveChangesCommand = new AsyncRelayCommand(_ => SaveChangesAsync());
            employee = itemsService.GetData<EditEmployeeVM, EmployeeDisplayModel>();
            itemsService.ClearData<EditEmployeeVM>();
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
            get => employee.DateOfBirth!.Value.ToDateTime(TimeOnly.MinValue);
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
            get => employee.Roles![0];
            set
            {
                if (employee.Roles![0] != value)
                {
                    employee.Roles![0] = value;
                    OnPropertyChanged(nameof(SelectedRole));
                }
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
        #endregion
        private async Task UploadPhotoAsync()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var photoBytes = File.ReadAllBytes(openFileDialog.FileName);
                employee.Photo = photoBytes;
                await photoRepository.UpdatePhotoAsync(employee.IdEmployee, photoBytes);
                OnPropertyChanged(nameof(Photo));
            }
        }
        private async Task LoadDataAsync()
        {
            Roles = await roleRepository.GetAsync();
            SelectedRole = Roles.FirstOrDefault(r => r.IdRole == employee.Roles![0].IdRole)!;
            employee.Photo = (await photoRepository.GetPhotoEmployeeAsync(employee.IdEmployee))!.PhotoEmployee1;
            OnPropertyChanged(nameof(Photo));
        }
        private async Task SaveChangesAsync()
        {
            await employeeRepository.UpdateEmployeeAsync(employee);
        }

    }
}
