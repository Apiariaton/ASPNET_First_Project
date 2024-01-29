using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Controllers;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data {

    public class NZWalksAuthDbContext : IdentityDbContext
    {

        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {


        }


    }





}