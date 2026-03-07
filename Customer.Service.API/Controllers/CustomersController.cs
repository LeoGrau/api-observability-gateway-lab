using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;


namespace Customer.Service.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class CustomersController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "Success", data = new { name = "Leonardo", id = 143 } });
    }
}