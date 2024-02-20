using Microsoft.AspNetCore.Mvc;

namespace LinkParserExample.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductsController : ControllerBase
{
    private LinkParser _linkParser;

    public ProductsController(LinkParser linkParser)
    {
        _linkParser = linkParser;
    }
    
    [HttpGet("{id}", Name = nameof(GetProduct))]
    public IActionResult GetProduct(int id)
    {
        return Ok($"Product {id}");
    }
    
    //test this with /api/products/getproduct/12
    [HttpGet]
    public IActionResult GetProductIdFromPath([FromQuery] string path)
    {
        var routeValues = _linkParser.ParsePathByEndpointName(nameof(GetProduct), path);
        return Ok($"The product id is {routeValues["id"]}");
    }
}