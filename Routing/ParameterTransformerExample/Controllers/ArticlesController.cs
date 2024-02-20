using Microsoft.AspNetCore.Mvc;

namespace ParameterTransformerExample.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ArticlesController : ControllerBase
{
    
    //test with /api/articles/getarticle/testartcilename
    [HttpGet("{name:slugify}", Name = "get")]
    public IActionResult GetArticle(string name)
    {
        return Ok($"The article name {name}");
    }

    [HttpGet(Name = "getlink")]
    public IActionResult GetLink()
    {
        //expected /api/Articles/GetArticle/the-quick-brown-fox
        //the name parameter is transformed by the Slugify parameter transformer
        return Ok(Url.Action("GetArticle", new { name = "TheQuickBrownFox" }));
    }
}