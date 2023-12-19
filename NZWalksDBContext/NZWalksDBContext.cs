using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{

    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {



        }


        public DbSet<Difficulty> Difficulties {get; set;}

        public DbSet<Region> Regions {get; set; }





    }





}