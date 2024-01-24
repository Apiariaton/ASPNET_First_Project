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

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true)
        {
            var listOfWalks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            
            //Filter by filterOn and filterQuery
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name"))
                {
                    listOfWalks = listOfWalks.Where(x => x.Name.Contains(filterQuery));
                }
                else if (filterOn.Equals("Difficulty"))
                {
                    listOfWalks = listOfWalks.Where(x=> x.Difficulty.Name.Equals(filterQuery));
                }
                else if (filterOn.Equals("Description"))
                {
                    listOfWalks = listOfWalks.Where(x=> x.Description.Contains(filterQuery));
                }
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy)==false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    listOfWalks = isAscending ? listOfWalks.OrderBy(x => x.Name) : listOfWalks.OrderByDescending(x => x.Name);

                }
                else if (sortBy.Equals("WalkLengthInKm"))
                {
                    listOfWalks = isAscending ? listOfWalks.OrderBy(x => x.WalkLengthInKm) : listOfWalks.OrderByDescending(x => x.WalkLengthInKm);
                }

            }

            return await listOfWalks.ToListAsync();
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {   
            var walkLocatedByID = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walkLocatedByID == null)
            {
                return null;
            }

            walkLocatedByID.Name = walk.Name;
            walkLocatedByID.Description = walk.Description;
            walkLocatedByID.WalkImageUrl = walk.WalkImageUrl;
            walkLocatedByID.WalkLengthInKm = walk.WalkLengthInKm;

            dbContext.SaveChangesAsync();
            return walkLocatedByID;

        }


        public async Task<Walk?> DeleteAsync(Guid id)
        {

            var walkLocatedByID = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walkLocatedByID == null)
            {
                return null;
            } 

            dbContext.Walks.Remove(walkLocatedByID);
            await dbContext.SaveChangesAsync();
            return walkLocatedByID;

        }

    
    }


}