using Microsoft.AspNetCore.Mvc;

namespace RequireHostExample.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ValuesController: ControllerBase
{
    [HttpGet]
    [ActionName("Msg")]
    [Host("localhost", "contoso.com")]
    public IActionResult GetMessage1()
    {
        return Ok("Msg");
    }
    
    [HttpGet]
    [ActionName("Msg2")]
    [Host("alibaba.com")]//can't go here unless from host alibaba.com
    public IActionResult GetMessage2()
    {
        return Ok("Msg2");
    }
}