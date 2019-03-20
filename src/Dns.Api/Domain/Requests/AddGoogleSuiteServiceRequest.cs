namespace Dns.Api.Domain.Requests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Dns.Domain.Services.GoogleSuite;
    using Dns.Domain.Services.GoogleSuite.Commands;
    using Swashbuckle.AspNetCore.Filters;

    public class AddGoogleSuiteServiceRequest
    {
        /// <summary>Service id of the manual service to add.</summary>
        [Required]
        public Guid ServiceId { get; set; }

        /// <summary>Label of the manual service to add.</summary>
        [Required]
        public string Label { get; set; }

        /// <summary>Verification token of the Google Suite service to add.</summary>
        [Required]
        public string VerificationToken { get; set; }
    }

    public class AddGoogleSuiteServiceRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddGoogleSuiteServiceRequest
            {
                ServiceId = Guid.NewGuid(),
                Label = ServiceType.googlesuite.DisplayName,
                VerificationToken = "rXOxyZounnZasA8Z7oaD3c14JdjS9aKSWvsR1EbUSIQ"
            };
        }
    }

    public static class AddGoogleSuiteServiceRequestMapping
    {
        public static AddGoogleSuite Map(
            DomainName domainName,
            AddGoogleSuiteServiceRequest message)
        {
            return new AddGoogleSuite(
                domainName,
                new ServiceId(message.ServiceId),
                new GoogleVerificationToken(message.VerificationToken));
        }
    }
}
