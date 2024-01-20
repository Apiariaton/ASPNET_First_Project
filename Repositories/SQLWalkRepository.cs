using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{

    public class SQLWalksRepository : IWalksRepository
    {

        private readonly NZWalksDbContext dbContext;
    
        public SQLWalksRepository(NZWalksDbContext dbContext)
        {   
            this.dbContext = dbContext;
        }
    
        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }
    
    }


}