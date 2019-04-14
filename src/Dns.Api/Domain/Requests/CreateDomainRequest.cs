namespace Dns.Api.Domain.Requests
{
    using System.ComponentModel.DataAnnotations;
    using Dns.Domain.Commands;
    using FluentValidation;
    using Swashbuckle.AspNetCore.Filters;

    public class CreateDomainRequest
    {
        /// <summary>Second level domain of the domain to register.</summary>
        [Required]
        [Display(Name = "Second Level Domain")]
        public string SecondLevelDomain { get; set; }

        /// <summary>Top level domain of the domain to register.</summary>
        [Required]
        [Display(Name = "Top Level Domain")]
        public string TopLevelDomain { get; set; }
    }

    public class CreateDomainRequestValidator : AbstractValidator<CreateDomainRequest>
    {
        public CreateDomainRequestValidator()
        {
            RuleFor(x => x.SecondLevelDomain)
                .Required()
                .ValidHostName()
                .MaxLength(SecondLevelDomain.MaxLength);

            RuleFor(x => x.TopLevelDomain)
                .Required()
                .ValidTopLevelDomain();
        }
    }

    public class CreateDomainRequestExample : IExamplesProvider
    {
        public object GetExamples()
            => new CreateDomainRequest
            {
                SecondLevelDomain = "exira",
                TopLevelDomain = TopLevelDomain.com.Value
            };
    }

    public static class CreateDomainRequestMapping
    {
        public static CreateDomain Map(
            CreateDomainRequest message)
            => new CreateDomain(
                new DomainName(
                    new SecondLevelDomain(message.SecondLevelDomain),
                    TopLevelDomain.FromValue(message.TopLevelDomain)));
    }
}
