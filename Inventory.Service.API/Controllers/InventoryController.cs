using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;


namespace Customer.Service.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class InventoryController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "Success", data = new { name = "TEMU IPhone 12", id = 2372 } });
    }

    
    [HttpPost]
    public IActionResult Post([FromBody] CreateProductRequest request)
    {
        return Ok(new { message = "Success in POST", data = new { request.Name } });
    }
}