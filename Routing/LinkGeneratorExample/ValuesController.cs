using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkGeneratorExample
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private LinkGenerator _linkGenerator { get;set;}

        public ValuesController(LinkGenerator linkGen)
        {
            _linkGenerator = linkGen;
        }

        [HttpGet]
        public string Get()
        {
            return _linkGenerator.GetPathByAction("Get", "Values");;
        }
    }
}
