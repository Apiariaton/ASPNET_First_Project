using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    // https:/localhost:3000/api/regions
    [Route("api/[controller]")]
    [ApiController]

    public class RegionsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = new List<Region>
            {
                new Region
                {
                Id = Guid.NewGuid(),
                Name = "AucklandRegion",
                Code = "AKL",
                RegionImageUrl = "blank1",
                },
                new Region 
                {
                    Id = Guid.NewGuid(),
                    Name = "Wellington Region",
                    Code = "WLG",
                    RegionImageUrl = "blank2"
                }
            };
        
            return Ok(regions);
        }

    }


}