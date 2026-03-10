using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Shared.Helpers;

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
    public IActionResult Post([FromBody] CreateCustomerRequest requestBody)
    {
        DataDogTracerHelper.CaptureHttpRequestBody(requestBody);
        DataDogTracerHelper.CaptureHttpRequestHeaders(HttpContext.Request);
        
        
        var responseBody = new { message = "Success in POST", data = new { requestBody.Name } };

        DataDogTracerHelper.CaptureHttpResponseBody(responseBody);
        DataDogTracerHelper.CaptureHttpResponseHeaders(HttpContext.Response);

        return Ok(responseBody);
    }
}