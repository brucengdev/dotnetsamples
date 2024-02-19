using Microsoft.AspNetCore.Mvc;

namespace AmbientVsExplicitRouteValues
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private LinkGenerator _linkGenerator { get; set; }

        public ValuesController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        [HttpGet("AmbientAndExplicit")]
        public IActionResult AmbientAndExplicit()
        {
            //the returned value is /api/Values?id=17
            //this is an example of ambient value, the path is retrieved from the HttpContext so
            //it is an ambient value that is specific to the current Http request.
            var indexPath = _linkGenerator.GetPathByAction(HttpContext, values: new { id = 17 });
            return Content(indexPath);
        }

        [HttpGet("ExplicitOnly")]
        public string ExplicitValue()
        {
            //example of explicit values
            //the following returns an URL by passing explicit values to the link generator
            //the HttpContext is not used at all, so no ambient values are used.
            return _linkGenerator.GetPathByAction("AmbientAndExplicit", "Values", new { id = 18 });
        }
        
    }
}
