using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace DependencyInjection.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IScopedDep _scopedDep;
        private readonly ISingletonDep _singletonDep;
        private readonly ISingletonDep _singletonDep2;
        private readonly ITransientDep _transientDep1, _transientDep2;
        private readonly ServiceScopeFactoryExample _scopeFactoryExample;

        public IndexModel(ILogger<IndexModel> logger,
            IScopedDep scopedService,
            IEnumerable<ISingletonDep> singletonDeps,
            //it is possible to resolve multiple registered services
            //services are passed in the order they were registered
            ITransientDep transientDep1,
            ITransientDep transientDep2,
            ServiceScopeFactoryExample scopeFactoryExample)
        {
            Trace.Assert(singletonDeps.Count() == 2);
            _logger = logger;
            _scopedDep = scopedService;
            var enumerator = singletonDeps.GetEnumerator();
            enumerator.MoveNext();
            _singletonDep = enumerator.Current;
            enumerator.MoveNext();
            _singletonDep2 = enumerator.Current;
            _transientDep1 = transientDep1;
            _transientDep2 = transientDep2;
            _scopeFactoryExample = scopeFactoryExample;
        }

        public void OnGet()
        {
            ViewData["SingletonDepMsg"] = _singletonDep.GetMessage();
            ViewData["SingletonDep2Msg"] = _singletonDep2.GetMessage();
            ViewData["ScopedDepMsg"] = _scopedDep.GetMessage();
            ViewData["TransientDep1Msg"] = _transientDep1.GetMessage();
            ViewData["TransientDep2Msg"] = _transientDep2.GetMessage();
            ViewData["MsgFromServiceCreatedFromScope"] = _scopeFactoryExample.GetMessage();
        }
    }
}