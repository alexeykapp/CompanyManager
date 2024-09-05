using CompanyManager.Database;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Repositories
{
    public class PhotoRepository(ApplicationContext applicationContext)
    {
        public async Task<PhotoEmployee?> GetPhotoEmployeeAsync(int idEmployee)
        {
            return await applicationContext.PhotoEmployees
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.FkEmployee == idEmployee);
        }
        public async Task UpdatePhotoAsync(int idEmployee, byte[] bytes)
        {
            var photo = await applicationContext.PhotoEmployees
                .AsNoTracking()
                .FirstOrDefaultAsync(pe => pe.FkEmployee == idEmployee);
            if (photo != null)
            {
                photo.PhotoEmployee1 = bytes;
                applicationContext.Update(photo);
            }
            else
            {
                await applicationContext.PhotoEmployees.AddAsync(new PhotoEmployee { PhotoEmployee1 = bytes, FkEmployee = idEmployee });
            }
            await applicationContext.SaveChangesAsync();
        }
    }
}
