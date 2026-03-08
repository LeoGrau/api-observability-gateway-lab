using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;


namespace Customer.Service.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class OrdersController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "Success in GET", data = new { name = "Sale FG000-12", id = 621 } });
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateOrderRequest request)
    {
        return Ok(new { message = "Success in POST", data = new { request.Name } });
    }
}