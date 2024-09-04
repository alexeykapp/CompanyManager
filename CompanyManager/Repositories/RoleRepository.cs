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
    }
}
