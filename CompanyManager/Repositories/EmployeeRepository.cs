using CompanyManager.Database;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Repositories
{
    public class EmployeeRepository(ApplicationContext applicationContext)
    {
        public async Task<List<Employee>> Get()
        {
            return await applicationContext.Employees
                .AsNoTracking()
                .OrderBy(x => x.FirstName)
                .ToListAsync();
        }
        public async Task<Employee?> GetById(int id)
        {
            return await applicationContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdEmployee == id);
        }
        public async Task Add(Employee employee)
        {
            await applicationContext.AddAsync(employee);
            await applicationContext.SaveChangesAsync();
        }
    }
}
