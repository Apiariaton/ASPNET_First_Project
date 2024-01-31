using Microsoft.EntityFrameworkCore;
using NZWalks.API.Controllers;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{

    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext>  dbContextOptions) : base(dbContextOptions)
        {
            


        }

        public DbSet<Difficulty> Difficulties {get; set;}

        public DbSet<Region> Regions {get; set; }


        public DbSet<Walk> Walks {get;set;}


        public DbSet<Image> Images {get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var walkDifficultyCategories = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("b37d3815-9d7f-44ae-9f05-1baf797a4827"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("7d65def3-951c-4096-b5c4-53cfe22781ac"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("9fd5c601-84ea-4524-8ab3-a3824ce75d06"),
                    Name = "Hard"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(walkDifficultyCategories);


            //Seed data for regions

            var regions = new List<Region>{

                new Region 
                {
                    Id = Guid.Parse("6f771621-ad5d-4da5-8df4-4c35f1b25c8f"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "some_image_url.jpeg",
                },

                new Region
                {
                     Id = Guid.Parse("885b4079-8dcf-4ad3-9ea2-931123debac6"),
                    Name = "Wellington",
                    Code = "WLN",
                    RegionImageUrl = "some_image_url.jpeg",

                },

                new Region 
                {
                     Id = Guid.Parse("05a918ea-c136-4af3-96d9-642da12cef54"),
                    Name = "Otago",
                    Code = "OTG",
                    RegionImageUrl = "some_image_url.jpeg",
                }

            };

            modelBuilder.Entity<Region>().HasData(regions);

        }



    }

}