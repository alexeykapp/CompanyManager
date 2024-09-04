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
            var employees = await applicationContext.Employees
                .AsNoTracking()
                .Include(e => e.EmployeeRoles)
                .ThenInclude(er => er.FkRole1)
                .ToListAsync();

            var employeeDisplayModels = employees.Select(e => new EmployeeDisplayModel
            {
                IdEmployee = e.IdEmployee,
                FirstName = e.FirstName,
                MiddleName = e.MiddleName,
                LastName = e.LastName,
                DateOfBirth = e.DateOfBirth,
                Phone = e.Phone,
                Passport = e.Passport,
                Address = e.Address,
                RoleName = string.Join(", ", e.EmployeeRoles.Select(er => er.FkRole1!.NameRole))
            }).ToList();

            return employeeDisplayModels;
        }
    }
}
