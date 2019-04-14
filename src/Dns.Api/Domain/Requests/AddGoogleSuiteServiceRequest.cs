namespace Dns.Api.Domain.Requests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Dns.Domain.Services.GoogleSuite;
    using Dns.Domain.Services.GoogleSuite.Commands;
    using FluentValidation;
    using Swashbuckle.AspNetCore.Filters;

    public class AddGoogleSuiteServiceRequest
    {
        /// <summary>Service id of the manual service to add.</summary>
        [Required]
        public Guid? ServiceId { get; set; }

        /// <summary>Label of the manual service to add.</summary>
        [Required]
        public string Label { get; set; }

        /// <summary>Verification token of the Google Suite service to add.</summary>
        [Required]
        public string VerificationToken { get; set; }

        [Required]
        [Display(Name = "Second Level Domain")]
        internal string SecondLevelDomain { get; set; }

        [Required]
        [Display(Name = "Top Level Domain")]
        internal string TopLevelDomain { get; set; }
    }

    public class AddGoogleSuiteServiceRequestValidator : AbstractValidator<AddGoogleSuiteServiceRequest>
    {
        public AddGoogleSuiteServiceRequestValidator()
        {
            RuleFor(x => x.SecondLevelDomain)
                .Required()
                .ValidHostName()
                .MaxLength(SecondLevelDomain.MaxLength);

            RuleFor(x => x.TopLevelDomain)
                .Required()
                .ValidTopLevelDomain();

            RuleFor(x => x.ServiceId)
                .Required();

            RuleFor(x => x.Label)
                .Required()
                .MaxLength(ServiceLabel.MaxLength);

            RuleFor(x => x.VerificationToken)
                .Required()
                .MaxLength(GoogleVerificationToken.MaxLength);
        }
    }

    public class AddGoogleSuiteServiceRequestExample : IExamplesProvider
    {
        public object GetExamples()
            => new AddGoogleSuiteServiceRequest
            {
                ServiceId = Guid.NewGuid(),
                Label = ServiceType.googlesuite.DisplayName,
                VerificationToken = "rXOxyZounnZasA8Z7oaD3c14JdjS9aKSWvsR1EbUSIQ"
            };
    }

    public static class AddGoogleSuiteServiceRequestMapping
    {
        public static AddGoogleSuite Map(
            DomainName domainName,
            AddGoogleSuiteServiceRequest message)
            => new AddGoogleSuite(
                domainName,
                new ServiceId(message.ServiceId.Value),
                new GoogleVerificationToken(message.VerificationToken));
    }
}
