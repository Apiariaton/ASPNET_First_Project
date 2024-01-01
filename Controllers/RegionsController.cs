using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;

namespace NZWalks.API.Controllers
{
    // https:/localhost:3000/api/regions
    [Route("api/[controller]")]
    [ApiController]

    public class RegionsController : ControllerBase
    {

        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var regions = dbContext.Regions.ToList();

            // var regions = new List<Region>
            // {
            //     new Region
            //     {
            //     Id = Guid.NewGuid(),
            //     Name = "AucklandRegion",
            //     Code = "AKL",
            //     RegionImageUrl = "blank1",
            //     },
            //     new Region 
            //     {
            //         Id = Guid.NewGuid(),
            //         Name = "Wellington Region",
            //         Code = "WLG",
            //         RegionImageUrl = "blank2"
            //     }
            // };

            return Ok(regions);
        }

        //Get region by id
        //Return Not Found if not found
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {

            //Alternative for properties which are not ID:
            //x.Id / x.Name
            //var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            //Only works for Id
            var region = dbContext.Regions.Find(id);
            if (region == null)
            {

                return NotFound();

            }
            
            return Ok(region);

        }
    }


}