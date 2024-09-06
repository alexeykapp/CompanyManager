using CompanyManager.Database;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Repositories
{
    public class PhotoRepository(ApplicationContext applicationContext)
    {
        public async Task<PhotoEmployee?> GetPhotoEmployeeAsync(int employeeId)
        {
            return await applicationContext.PhotoEmployees
                .FirstOrDefaultAsync(p => p.FkEmployee == employeeId);
        }

        public async Task UpdatePhotoAsync(int employeeId, byte[] photoBytes)
        {
            var photo = await applicationContext.PhotoEmployees
                .FirstOrDefaultAsync(pe => pe.FkEmployee == employeeId);

            if (photo != null)
            {
                photo.PhotoEmployee1 = photoBytes;
            }
            else
            {
                photo = new PhotoEmployee
                {
                    PhotoEmployee1 = photoBytes,
                    FkEmployee = employeeId
                };

                applicationContext.PhotoEmployees.Add(photo);
            }

            await applicationContext.SaveChangesAsync();
        }
    }
}