namespace Dns.Api.Domain.Requests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using FluentValidation;

    public class DetailServiceRequest
    {
        /// <summary>Second level domain of the domain to request details of a domain service for.</summary>
        [Required]
        [Display(Name = "Second Level Domain")]
        public string SecondLevelDomain { get; set; }

        /// <summary>Top level domain of the domain to request details of a domain service for.</summary>
        [Required]
        [Display(Name = "Top Level Domain")]
        public string TopLevelDomain { get; set; }

        /// <summary>Unique service id to request details for.</summary>
        [Required]
        [Display(Name = "Service Id")]
        public Guid? ServiceId { get; set; }
    }

    public class DetailServiceRequestValidator : AbstractValidator<DetailServiceRequest>
    {
        public DetailServiceRequestValidator()
        {
            RuleFor(x => x.SecondLevelDomain)
                .Required()
                .ValidHostName()
                .MaxLength(SecondLevelDomain.MaxLength);

            RuleFor(x => x.TopLevelDomain)
                .Required()
                .ValidTopLevelDomain();

            RuleFor(x => x.ServiceId)
                .NotNull()
                .NotEmpty();
        }
    }
}
