using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using NZWalks.API.Data;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;



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
    
    var walkCreatedFromPostRequest = new Walk{
        Name = newWalkTemplateDTO.Name,
        Description = newWalkTemplateDTO.Description,
        WalkLengthInKm = newWalkTemplateDTO.WalkLengthInKm,
        WalkImageUrl = newWalkTemplateDTO.WalkImageUrl,
        Difficulty = newWalkTemplateDTO.Difficulty,
        Region = newWalkTemplateDTO.Region
    };


    walkCreatedFromPostRequest= await walksRepository.CreateAsync(walkCreatedFromPostRequest);


    var walkCreatedFromPostRequestDTO = new Walk{
        Id = walkCreatedFromPostRequest.Id,
        Name = walkCreatedFromPostRequest.Name,
        Description = walkCreatedFromPostRequest.Description,
        WalkLengthInKm = walkCreatedFromPostRequest.WalkLengthInKm,
        WalkImageUrl = walkCreatedFromPostRequest.WalkImageUrl,
        Difficulty = walkCreatedFromPostRequest.Difficulty,
        Region = walkCreatedFromPostRequest.Region
    };

    return CreatedAtAction("GetByIdAsync", new {id = walkCreatedFromPostRequestDTO.Id}, walkCreatedFromPostRequestDTO);

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

        
    }








}