using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{

    public class SQLRegionRepository : IRegionRepository
    {

        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync (Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> CreateAsync (Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync (Guid id, Region region)
        {
            var regionLocatedById = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionLocatedById == null)
            {
                return null;
            }

            regionLocatedById.Code = region.Code;
            regionLocatedById.Name = region.Name;
            regionLocatedById.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return regionLocatedById;
        }


        public async Task<Region?> DeleteAsync(Guid id)
        {
            var regionLocatedById = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionLocatedById == null)
            {
                return null;
            }

            dbContext.Regions.Remove(regionLocatedById);
            await dbContext.SaveChangesAsync();
            return regionLocatedById;
        }

     

    }


}