using CompanyManager.Base;
using CompanyManager.Converters;
using CompanyManager.Database;
using CompanyManager.DataBase.DisplayModel;
using CompanyManager.Interfaces;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CompanyManager.ViewModel
{
    public class EditEmployeeVM : BaseViewModel
    {
        private EmployeeDisplayModel employee;
        private IItemsService itemsService;
        private Role selectedRole;
        private ByteImage byteImage;
        public ObservableCollection<Role> Roles { get; set; }
        public ObservableCollection<Role> SelectedRoles { get; set; }

        public EditEmployeeVM(IItemsService itemsService, ByteImage byteImage)
        {
            this.itemsService = itemsService;
            this.byteImage = byteImage;
            UploadPhotoCommand = new RelayCommand(_ => UploadPhoto());
            employee = itemsService.GetData<EditEmployeeVM, EmployeeDisplayModel>();
            itemsService.ClearData<EditEmployeeVM>();
        }
        public RelayCommand UploadPhotoCommand { get; set; }
        public AsyncRelayCommand SaveEditCommand { get; set; }
        #region PROPERTIES
        public string FirstName
        {
            get => employee.FirstName;
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
            get => employee.LastName;
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
            get => employee.MiddleName;
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
            get => employee.Phone;
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
            get => employee.Passport;
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
            get => employee.Address;
            set
            {
                if (employee.Address != value)
                {
                    employee.Address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        public string RoleName
        {
            get => employee.RoleName;
            set
            {
                if (employee.RoleName != value)
                {
                    employee.RoleName = value;
                    OnPropertyChanged(nameof(RoleName));
                }
            }
        }
        public Role SelectedRole
        {
            get => selectedRole;
            set
            {
                if (selectedRole != value)
                {
                    selectedRole = value;
                    OnPropertyChanged();
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
        #endregion
        private void UploadPhoto()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var photoBytes = File.ReadAllBytes(openFileDialog.FileName);
                employee.Photo = photoBytes;
                OnPropertyChanged(nameof(Photo));
            }
        }
    }
}
