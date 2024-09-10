using CompanyManager.Database;
using CompanyManager.DataBase.DisplayModel;

namespace CompanyManager.Converters
{
    public class EmployeeDisplayModelConverter
    {
        public Employee ConvertToEmployee(EmployeeDisplayModel employeeDisplayModel) 
        {
            var employee = new Employee
            {
                IdEmployee = employeeDisplayModel.IdEmployee,
                FirstName = employeeDisplayModel.FirstName,
                MiddleName = employeeDisplayModel.MiddleName,
                LastName = employeeDisplayModel.LastName,
                DateOfBirth = employeeDisplayModel.DateOfBirth,
                Phone = employeeDisplayModel.Phone,
                Passport = employeeDisplayModel.Passport,
                Address = employeeDisplayModel.Address
            };

            return employee;
        }
    }
}
