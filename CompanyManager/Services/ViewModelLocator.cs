using CompanyManager.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManager.Services
{
    public class ViewModelLocator(IServiceProvider serviceProvider)
    {
        public StartWindowVM StartWindowVM => serviceProvider.GetRequiredService<StartWindowVM>();
        public EmployeeWindowVM EmployeeWindowVM => serviceProvider.GetRequiredService<EmployeeWindowVM>();
        public AddEmployeeVM AddEmployeeVM => serviceProvider.GetRequiredService<AddEmployeeVM>();
        public EditEmployeeVM EditEmployeeVM => serviceProvider.GetRequiredService<EditEmployeeVM>();
    }
}
