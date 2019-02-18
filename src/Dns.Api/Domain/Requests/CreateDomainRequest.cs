namespace Dns.Api.Domain.Requests
{
    using System.ComponentModel.DataAnnotations;
    using Dns.Domain.Commands;
    using Swashbuckle.AspNetCore.Filters;

    public class CreateDomainRequest
    {
        /// <summary>Second level domain of the domain to register.</summary>
        [Required]
        public string SecondLevelDomain { get; set; }

        /// <summary>Top level domain of the domain to register.</summary>
        [Required]
        public string TopLevelDomain { get; set; }
    }

    public class CreateDomainRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new CreateDomainRequest
            {
                SecondLevelDomain = "exira",
                TopLevelDomain = TopLevelDomain.com.Value
            };
        }
    }

    public static class CreateDomainRequestMapping
    {
        public static CreateDomain Map(CreateDomainRequest message)
        {
            return new CreateDomain(
                new DomainName(
                    new SecondLevelDomain(message.SecondLevelDomain),
                    TopLevelDomain.FromValue(message.TopLevelDomain)));
        }
    }
}
