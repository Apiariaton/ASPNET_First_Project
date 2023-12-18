using Microsoft.AspNetCore.Mvc;

namespace NZWalks.Controllers;


[ApiController]
[Route("[controller]")]
public class TreesController : ControllerBase 
{

    [HttpGet]
    public IActionResult getTrees()
    {
    string [] trees = new string[] {"Oak","Chestnut","Camphor","Birch"};
    return Ok(trees);
    }

    // public IActionResult getTreeSpecies()
    // {



    // }








};