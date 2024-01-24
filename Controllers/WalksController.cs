using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using NZWalks.API.Data;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.CustomActionFilters;



namespace NZWalks.API.Controllers
{

[Route("/api/[controller]")]
[ApiController]

public class WalksController : ControllerBase 
{

    private readonly NZWalksDbContext dbContext;
    private readonly IWalksRepository walksRepository;

    public WalksController(NZWalksDbContext dbContext, IWalksRepository walksRepository)
    {
        this.dbContext = dbContext;
        this.walksRepository = walksRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NewWalkTemplateDTO newWalkTemplateDTO)
    {
    
    if (ModelState.IsValid)
    {
    var walkCreatedFromPostRequest = new Walk{
        Name = newWalkTemplateDTO.Name,
        Description = newWalkTemplateDTO.Description,
        WalkLengthInKm = newWalkTemplateDTO.WalkLengthInKm,
        WalkImageUrl = newWalkTemplateDTO.WalkImageUrl,
        DifficultyId = newWalkTemplateDTO.DifficultyId,
        RegionId = newWalkTemplateDTO.RegionId
    };


    walkCreatedFromPostRequest= await walksRepository.CreateAsync(walkCreatedFromPostRequest);


    var walkCreatedFromPostRequestDTO = new Walk{
        Id = walkCreatedFromPostRequest.Id,
        Name = walkCreatedFromPostRequest.Name,
        Description = walkCreatedFromPostRequest.Description,
        WalkLengthInKm = walkCreatedFromPostRequest.WalkLengthInKm,
        WalkImageUrl = walkCreatedFromPostRequest.WalkImageUrl,
        DifficultyId = walkCreatedFromPostRequest.DifficultyId,
        RegionId = walkCreatedFromPostRequest.RegionId
    };

    return CreatedAtAction("GetByIdAsync", new {id = walkCreatedFromPostRequestDTO.Id}, walkCreatedFromPostRequestDTO);
    }
    else
    {
        return BadRequest(ModelState);
    }
    } 


    [HttpGet]
    [Route("{id:Guid}")]
    [ActionName(nameof(GetByIdAsync))]

    public async Task<IActionResult> GetByIdAsync(Guid id)
    {

        var walkInstance = await walksRepository.GetByIdAsync(id);

        if (walkInstance == null)
        {
            return NotFound();
        }

        var walkDataTransferObject = new WalkDto{
            Id = walkInstance.Id,
            Name = walkInstance.Name,
            Description = walkInstance.Description,
            WalkLengthInKm = walkInstance.WalkLengthInKm,
            WalkImageUrl = walkInstance.WalkImageUrl,
            DifficultyId = walkInstance.DifficultyId,
            RegionId = walkInstance.RegionId,
            Difficulty = walkInstance.Difficulty,
            Region = walkInstance.Region
        };

        return Ok(walkDataTransferObject);

    }
  
    //GET Walks
    //Get:/api/walks?filterOn=Name&filterQuery=Wellington

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
    {
        var listOfWalks = await walksRepository.GetAllAsync(filterOn,filterQuery);

        var walksDataTransferObject = new List<WalkDto>();
        foreach (var walk in listOfWalks)
        {
            walksDataTransferObject.Add(
                new WalkDto(){
                    Id = walk.Id,
                    Name = walk.Name,
                    Description = walk.Description,
                    WalkLengthInKm = walk.WalkLengthInKm,
                    WalkImageUrl = walk.WalkImageUrl,
                    DifficultyId = walk.DifficultyId,
                    RegionId = walk.RegionId,
                    Difficulty = walk.Difficulty,
                    Region = walk.Region    
                }
            );
        }

        return Ok(walksDataTransferObject);
    }


    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateWalkTemplateDto updateWalkTemplateDto)
    {
        var modelToUpdateWalkByID = new Walk{
            Name = updateWalkTemplateDto.Name,
            Description = updateWalkTemplateDto.Description,
            WalkImageUrl = updateWalkTemplateDto.WalkImageUrl,
            WalkLengthInKm = updateWalkTemplateDto.WalkLengthInKm,
            RegionId = updateWalkTemplateDto.RegionId,
            DifficultyId = updateWalkTemplateDto.DifficultyId
        };


        modelToUpdateWalkByID = await walksRepository.UpdateAsync(id, modelToUpdateWalkByID);


        if (modelToUpdateWalkByID == null)
        {
            return NotFound();
        }

        var changedWalkDto = new WalkDto{
            Name = modelToUpdateWalkByID.Name,
            Description = modelToUpdateWalkByID.Description,
            WalkImageUrl = modelToUpdateWalkByID.WalkImageUrl,
            WalkLengthInKm = modelToUpdateWalkByID.WalkLengthInKm,
            RegionId = modelToUpdateWalkByID.RegionId,
            DifficultyId = modelToUpdateWalkByID.DifficultyId
        };

        return Ok(changedWalkDto);
        }
    


    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id){

        var walkLocatedByID = await walksRepository.DeleteAsync(id);

        if (walkLocatedByID == null)
        {

            return NotFound();

        }

        var deletedWalkDTO = new WalkDto{
            Name = walkLocatedByID.Name,
            Description = walkLocatedByID.Description,
            WalkLengthInKm = walkLocatedByID.WalkLengthInKm,
            WalkImageUrl = walkLocatedByID.WalkImageUrl,
            RegionId = walkLocatedByID.RegionId,
            DifficultyId = walkLocatedByID.DifficultyId
        };

        return Ok(deletedWalkDTO);

    }



    }
}