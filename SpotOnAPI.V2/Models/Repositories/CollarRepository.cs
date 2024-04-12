using Microsoft.EntityFrameworkCore;
using SpotOnAPI.V2.Interfaces;

namespace SpotOnAPI.V2.Models.Repositories
{
    public class CollarRepository : ICollarRepository
    {
        public readonly SpotOnDbContext _databaseContext;

        public CollarRepository(SpotOnDbContext dBcontext)
        {
            _databaseContext = dBcontext;
        }

        public async Task<Collar> CreateCollar(Collar collar)
        {
            _databaseContext.Collars.Add(collar);
            await _databaseContext.SaveChangesAsync();

            return collar;
        }

        public async Task<bool> DeleteCollar(Guid collarId)
        {
            var rows = await _databaseContext.Collars.Where(x => x.CollarId == collarId).ExecuteDeleteAsync();

            return true;
        }

        public async Task<Collar> EditCollar(Collar collar)
        {
            var dbCollar = await _databaseContext.Collars.Where(x => x.CollarId == collar.CollarId)
                .ExecuteUpdateAsync(x => x.SetProperty(x => x.UserId, collar.UserId)
                    .SetProperty(x => x.Nickname, collar.Nickname)
                    .SetProperty(x => x.Latitude, collar.Latitude)
                    .SetProperty(x => x.Longitude, collar.Longitude)
                    .SetProperty(x => x.Timestamp, collar.Timestamp)
                    );

            return collar;
        }

        public async Task<List<Collar>> GetCollars()
        {
            var result = await _databaseContext.Collars.ToListAsync();

            return result;
        }

        public async Task<List<Collar>> GetCustomerCollars(int id)
        {
            var result = await _databaseContext.Collars.Where(x => x.UserId == id).ToListAsync();

            return result;
        }

    }
}
