namespace Dns.Api.Example.Requests
{
    using System.ComponentModel.DataAnnotations;
    using Domain.Commands;
    using Newtonsoft.Json;
    using Swashbuckle.AspNetCore.Filters;

    public class DomainRequest
    {
        /// <summary>Name of the domain to register.</summary>
        [Required]
        public string Name { get; set; }

        /// <summary>Top level domain of the domain to register.</summary>
        [Required]
        public TopLevelDomain TopLevelDomain { get; set; }
    }

    public class DomainRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new DomainRequest
            {
                Name = "exira",
                TopLevelDomain = TopLevelDomain.com
            };
        }
    }

    public static class DomainRequestMapping
    {
        public static CreateDomain Map(DomainRequest message)
        {
            return new CreateDomain(
                new DomainName(message.Name, message.TopLevelDomain));
        }
    }
}
