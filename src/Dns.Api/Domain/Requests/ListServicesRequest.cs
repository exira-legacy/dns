namespace Dns.Api.Domain.Requests
{
    using System.ComponentModel.DataAnnotations;
    using FluentValidation;

    public class ListServicesRequest
    {
        /// <summary>Second level domain of the domain to list services for.</summary>
        [Required]
        [Display(Name = "Second Level Domain")]
        public string SecondLevelDomain { get; set; }

        /// <summary>Top level domain of the domain to list services for.</summary>
        [Required]
        [Display(Name = "Top Level Domain")]
        public string TopLevelDomain { get; set; }
    }

    public class ListServicesRequestValidator : AbstractValidator<ListServicesRequest>
    {
        public ListServicesRequestValidator()
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
}
