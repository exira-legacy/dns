namespace Dns.Api.Infrastructure
{
    using System.Reflection;
    using System.Threading;
    using Be.Vlaanderen.Basisregisters.Api;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Microsoft.Net.Http.Headers;

    [ApiVersionNeutral]
    [Route("")]
    public class EmptyController : ApiController
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Get(
            [FromServices] IHostingEnvironment hostingEnvironment,
            [FromServices] IStringLocalizer<EmptyController> localizer,
            CancellationToken cancellationToken)
            => Request.Headers[HeaderNames.Accept].ToString().Contains("text/html")
                ? (IActionResult)new RedirectResult("/docs")
                : new OkObjectResult($"{localizer["Welcome to the Dns Api v{0}.", Assembly.GetEntryAssembly().GetName().Version]}");
    }
}
