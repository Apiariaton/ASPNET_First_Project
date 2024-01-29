using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Controllers;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data {

    public class NZWalksAuthDbContext : IdentityDbContext
    {

        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var ReaderRoleID = "dbcf2358-cbc4-4aca-a99e-6eafe4447674";
            var WriterRoleID = "6c6729c0-6561-4c20-b4da-3fed150360d2";



            var ListOfUserRolesToAccessAPI = new List<IdentityRole>{

                new IdentityRole
                {
                    Id = ReaderRoleID,
                    ConcurrencyStamp = ReaderRoleID,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()   
                },

                new IdentityRole
                {
                    Id = WriterRoleID,
                    ConcurrencyStamp = WriterRoleID,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                },
            };

            builder.Entity<IdentityRole>().HasData(ListOfUserRolesToAccessAPI);
        }


    }





}