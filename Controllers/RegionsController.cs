using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

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

            var regionsList = dbContext.Regions.ToList();

            var regionsDataTransferObject = new List<RegionDto>();
            foreach (var region in regionsList)
            {
                regionsDataTransferObject.Add(new RegionDto(){
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });               
            };

            //Map regions to Data Transfer Object

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

            return Ok(regionsDataTransferObject);
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
            var regionInstance = dbContext.Regions.Find(id);
            
            
            if (regionInstance == null)
            {

                return NotFound();

            };

            //Map regionInstance to Data Transfer Object
            var regionDataTransferObject = new RegionDto{
                Id = regionInstance.Id,
                Code = regionInstance.Code,
                Name = regionInstance.Name,
                RegionImageUrl = regionInstance.RegionImageUrl
            };

            return Ok(regionDataTransferObject);

        }


        [HttpPost]
        public IActionResult Create([FromBody] NewRegionTemplateDto newRegionTemplateDto)
        {
            //Map or Convert DTO to Domain Model
            var regionCreatedFromPostRequest = new Region {
                Code = newRegionTemplateDto.Code,
                Name = newRegionTemplateDto.Name,
                RegionImageUrl = newRegionTemplateDto.RegionImageUrl
            };


            // Use Domain Model to create Region
            dbContext.Regions.Add(regionCreatedFromPostRequest);
            dbContext.SaveChanges();


            //Create a data transfer object for the newly created region
            var regionCreatedFromPostRequestDTO = new Region {
                Id = regionCreatedFromPostRequest.Id,
                Code = regionCreatedFromPostRequest.Code,
                Name = regionCreatedFromPostRequest.Name,
                RegionImageUrl = regionCreatedFromPostRequest.RegionImageUrl
            };

            //See: https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.createdataction?view=aspnetcore-8.0
            return CreatedAtAction(nameof(GetById), new {id = regionCreatedFromPostRequest.Id}, regionCreatedFromPostRequestDTO);

        }


    }

}