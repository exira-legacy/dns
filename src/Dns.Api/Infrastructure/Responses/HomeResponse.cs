namespace Dns.Api.Infrastructure.Responses
{
    using System.Collections.Generic;
    using System.Net;
    using System.Runtime.Serialization;
    using Swashbuckle.AspNetCore.Filters;

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
                new Link("/domains", Link.Relations.Domains, WebRequestMethods.Http.Get),
                new Link("/domains", Link.Relations.Domains, WebRequestMethods.Http.Post)
            };
        }
    }

    public class HomeResponseExamples : IExamplesProvider
    {
        public object GetExamples() => new HomeResponse();
    }
}
