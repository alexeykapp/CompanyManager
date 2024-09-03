using CompanyManager.Base;
using CompanyManager.View;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManager.ViewModel
{
    public class StartWindowVM : BaseViewModel
    {
        public RelayCommand NavigateEmployeesCommand { get; set; }
        private IServiceProvider serviceProvider {  get; set; }
        public StartWindowVM(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            NavigateEmployeesCommand = new RelayCommand(NavigateEmployeesMethod);
        }

        private void NavigateEmployeesMethod(object ob)
        {
            var employeeWindow = serviceProvider.GetRequiredService<EmployeeWindow>();
            employeeWindow.Show();
        }
    }
}
