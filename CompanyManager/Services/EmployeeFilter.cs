using CompanyManager.DataBase.DisplayModel;

namespace CompanyManager.Services
{
    public class EmployeeFilter
    {
        public List<EmployeeDisplayModel> FilterEmployees(List<EmployeeDisplayModel> employees, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return employees;

            string lowerSearchTerm = searchTerm.ToLower();

            return employees.Where(e =>
                (e.FirstName != null && e.FirstName.ToLower().Contains(lowerSearchTerm)) ||
                (e.MiddleName != null && e.MiddleName.ToLower().Contains(lowerSearchTerm)) ||
                (e.LastName != null && e.LastName.ToLower().Contains(lowerSearchTerm)) ||
                (e.Phone != null && e.Phone.ToLower().Contains(lowerSearchTerm)) ||
                (e.Passport != null && e.Passport.ToLower().Contains(lowerSearchTerm)) ||
                (e.Address != null && e.Address.ToLower().Contains(lowerSearchTerm)) ||
                (e.DateOfBirth != null && e.DateOfBirth.ToString()!.ToLower().Contains(lowerSearchTerm)) ||
                (e.Roles != null && e.Roles.Any(role => role.ToString()!.ToLower().Contains(lowerSearchTerm)))
            ).ToList();
        }
    }
}
