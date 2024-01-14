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
    private readonly IRegionRepository walksRepository;


    public WalksController(NZWalksDbContext dbContext, IRegionRepository walksRepository)
    {
        this.dbContext = dbContext;
        this.walksRepository = walksRepository;
    }


    
















}



















}