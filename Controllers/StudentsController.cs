using Microsoft.AspNetCore.Mvc;

namespace NZWalks.Controllers;

[ApiController]
[Route("[controller]")]

public class StudentsController : ControllerBase 
{
    // GET: https://localhost:portnumber/api/students
    [HttpGet]

    public IActionResult GetAllStudents()
    {
        string [] studentNames = new string[] {"John","Jane","Mark","Emily","David"};
        return Ok(studentNames);
    }

};