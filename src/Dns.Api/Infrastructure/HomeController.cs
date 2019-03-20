namespace Dns.Api.Infrastructure
{
    using System.Collections.Generic;
    using System.Net;
    using System.Runtime.Serialization;
    using Be.Vlaanderen.Basisregisters.Api;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [AdvertiseApiVersions("1.0")]
    [ApiRoute("")]
    [ApiExplorerSettings(GroupName = "Home")]
    public class HomeController : ApiController
    {
        [HttpGet]
        public IActionResult Get() => Ok(new HomeResponse());
    }

    [DataContract(Name = "Home", Namespace = "")]
    public class HomeResponse
    {
        /// <summary>
        /// Hypermedia links
        /// </summary>
        [DataMember(Name = "Links", Order = 1)]
        public List<Link> Links { get; set; }

        public HomeResponse()
        {
            Links = new List<Link>
            {
                new Link("/domains", Link.Relations.Domains, WebRequestMethods.Http.Get)
            };
        }
    }
}
