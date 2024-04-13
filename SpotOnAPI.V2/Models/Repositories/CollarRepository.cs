using System.Linq.Expressions;
using Microsoft.AspNetCore.Http.HttpResults;
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
            try
            {
                //These if statements verify lon and lat if fail useing userId to throw exception in controller.
                if (collar.Longitude < -180 || collar.Longitude > 180)
                {
                    Collar bad = new Collar();
                    bad.UserId = 99998;
                    return bad;
                }
                if (collar.Latitude < -90 || collar.Latitude > 90)
                {
                    Collar bad = new Collar();
                    bad.UserId = 99997;
                    return bad;
                }
                var temp = _databaseContext.Collars.Add(collar);
                var temp2 = await _databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Collar bad = new Collar();
                bad.UserId = 99999;
                return bad;
            }

            return collar;
        }

        public async Task<bool> DeleteCollar(Guid collarId)
        {
            var rows = await _databaseContext.Collars.Where(x => x.CollarId == collarId).ExecuteDeleteAsync();
            if (rows == 0)
                return false;
            return true;
        }

        public async Task<Collar> EditCollar(Collar collar)
        {
            //These if statements verify lon and lat if fail useing userId to throw exception in controller.
            if (collar.Longitude < -180 || collar.Longitude > 180)
            {
                Collar bad = new Collar();
                bad.UserId = 99998;
                return bad;
            }
            if (collar.Latitude < -90 || collar.Latitude > 90)
            {
                Collar bad = new Collar();
                bad.UserId = 99997;
                return bad;
            }

            var dbCollar = await _databaseContext.Collars.Where(x => x.CollarId == collar.CollarId)
                .ExecuteUpdateAsync(x => x.SetProperty(x => x.UserId, collar.UserId)
                    .SetProperty(x => x.Nickname, collar.Nickname)
                    .SetProperty(x => x.Latitude, collar.Latitude)
                    .SetProperty(x => x.Longitude, collar.Longitude)
                    .SetProperty(x => x.Timestamp, collar.Timestamp)
                    );

            if(dbCollar == 0)
            {
                Collar bad = new Collar();
                bad.UserId = 99999;
                return bad;
            }

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
