using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // https:/localhost:3000/api/regions
    [Route("api/[controller]")]
    [ApiController]

    public class RegionsController : ControllerBase
    {

        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            //Obtain data from regionRepository, which queries database
            var regionsList = await regionRepository.GetAllAsync();

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
         [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {

            //Alternative for properties which are not ID:
            //x.Id / x.Name
            //var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            //Only works for Id
            var regionInstance = await regionRepository.GetByIdAsync(id);
            
            
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
        public async Task<IActionResult> Create([FromBody] NewRegionTemplateDto newRegionTemplateDto)
        {
            //Map or Convert DTO to Domain Model
            var regionCreatedFromPostRequest = new Region{
                Code = newRegionTemplateDto.Code,
                Name = newRegionTemplateDto.Name,
                RegionImageUrl = newRegionTemplateDto.RegionImageUrl
            };


            // Use Domain Model to create Region
            regionCreatedFromPostRequest = await regionRepository.CreateAsync(regionCreatedFromPostRequest);


            //Create a data transfer object for the newly created region
            var regionCreatedFromPostRequestDTO = new Region {
                Id = regionCreatedFromPostRequest.Id,
                Code = regionCreatedFromPostRequest.Code,
                Name = regionCreatedFromPostRequest.Name,
                RegionImageUrl = regionCreatedFromPostRequest.RegionImageUrl
            };

            //See: https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.createdataction?view=aspnetcore-8.0
            return CreatedAtAction("GetByIdAsync", new {id = regionCreatedFromPostRequestDTO.Id}, regionCreatedFromPostRequestDTO);

        }

        
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRegionTemplateDto updateRegionTemplateDto)
        {
    
            var modelToUpdateRegionByID = new Region{
                Code = updateRegionTemplateDto.Code,
                Name = updateRegionTemplateDto.Name,
                RegionImageUrl = updateRegionTemplateDto.RegionImageUrl
            }; 

            modelToUpdateRegionByID = await regionRepository.UpdateAsync(id, modelToUpdateRegionByID);

            if (modelToUpdateRegionByID == null)
            {
                return NotFound();
            }

            var changedRegionDTO = new RegionDto{
                Id = modelToUpdateRegionByID.Id,
                Code = modelToUpdateRegionByID.Code,
                Name = modelToUpdateRegionByID.Name,
                RegionImageUrl = modelToUpdateRegionByID.RegionImageUrl
            };

            return Ok(changedRegionDTO);
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionLocatedWithRouteID = await regionRepository.DeleteAsync(id);

            if (regionLocatedWithRouteID == null)
            {
                return NotFound();
            }

            var deletedRegionDTO = new RegionDto{
                Id = regionLocatedWithRouteID.Id,
                Code = regionLocatedWithRouteID.Code,
                Name = regionLocatedWithRouteID.Name,
                RegionImageUrl = regionLocatedWithRouteID.RegionImageUrl
            };

            return Ok(deletedRegionDTO);
        }


    }

}