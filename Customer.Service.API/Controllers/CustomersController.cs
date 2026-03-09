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

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCustomerRequest requestBody)
    {
        await DataDogTracingHelper.CaptureHttpRequest(HttpContext.Request, requestBody);
        return Ok(new { message = "Success in POST", data = new { requestBody.Name } });
    }
}