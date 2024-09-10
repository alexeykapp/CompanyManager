using CompanyManager.Database;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Repositories
{
    public class RoleRepository(ApplicationContext applicationContext)
    {
        public async Task<List<Role>> GetAsync()
        {
            return await applicationContext.Roles
                 .AsNoTracking()
                 .ToListAsync();
        }
        public async Task AddRoleToEmployee(List<Role> roles, Employee employee)
        {
            foreach (Role role in roles)
            {
                employee.EmployeeRoles.Add(new EmployeeRole
                {
                    FkRole = role.IdRole,
                    FkEmployee = employee.IdEmployee
                });
            }
            await applicationContext.SaveChangesAsync();
        }
    }
}
