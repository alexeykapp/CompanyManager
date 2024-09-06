using CompanyManager.Database;
using CompanyManager.DataBase.DisplayModel;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Repositories
{
    public class EmployeeRepository(ApplicationContext applicationContext)
    {
        public async Task<List<Employee>> GetAsync()
        {
            return await applicationContext.Employees
                .AsNoTracking()
                .OrderBy(x => x.FirstName)
                .ToListAsync();
        }
        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await applicationContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdEmployee == id);
        }
        public async Task AddAsync(Employee employee)
        {
            await applicationContext.AddAsync(employee);
            await applicationContext.SaveChangesAsync();
        }
        public async Task<List<EmployeeDisplayModel>> GetWithRolesAsync()
        {
            return await applicationContext.Employees
                 .AsNoTracking()
                 .Include(e => e.EmployeeRoles)
                 .ThenInclude(er => er.FkRoleNavigation)
                 .Select(e => new EmployeeDisplayModel
                 {
                     IdEmployee = e.IdEmployee,
                     FirstName = e.FirstName,
                     MiddleName = e.MiddleName,
                     LastName = e.LastName,
                     DateOfBirth = e.DateOfBirth,
                     Phone = e.Phone,
                     Passport = e.Passport,
                     Address = e.Address,
                     Roles = e.EmployeeRoles.Select(x => x.FkRoleNavigation).ToList()!
                 }).ToListAsync();
        }
        public async Task UpdateEmployeeAsync(EmployeeDisplayModel employeeModel)
        {
            var employee = await applicationContext.Employees
                .AsNoTracking()
                .Include(e => e.EmployeeRoles)
                .FirstOrDefaultAsync(e => e.IdEmployee == employeeModel.IdEmployee);

            if (employee != null)
            {
                employee.FirstName = employeeModel.FirstName;
                employee.LastName = employeeModel.LastName;
                employee.MiddleName = employeeModel.MiddleName;
                employee.Phone = employeeModel.Phone;
                employee.Passport = employeeModel.Passport;
                employee.Address = employeeModel.Address;

                var role = await applicationContext.Roles
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.NameRole == employeeModel.Roles!.LastOrDefault()!.NameRole);
                if (role != null)
                {
                    employee.EmployeeRoles.Clear();
                    employee.EmployeeRoles.Add(new EmployeeRole { FkRole = role.IdRole });
                }

                applicationContext.Employees.Update(employee);
                await applicationContext.SaveChangesAsync();
            }
        }
    }
}