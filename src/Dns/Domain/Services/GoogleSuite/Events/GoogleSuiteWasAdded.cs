namespace Dns.Domain.Services.GoogleSuite.Events
{
    using System;
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Newtonsoft.Json;

    [EventName("GoogleSuiteWasAdded")]
    [EventDescription("The G Suite service was added.")]
    public class GoogleSuiteWasAdded
    {
        [JsonIgnore]
        public string DomainName { get; }
        public string SecondLevelDomain { get; }
        public string TopLevelDomain { get; }

        public Guid ServiceId { get; }
        public string ServiceType { get; }
        public string ServiceLabel { get; }

        public string VerificationToken { get; }

        public GoogleSuiteWasAdded(
            DomainName domainName,
            ServiceId serviceId,
            GoogleVerificationToken verificationToken)
        {
            DomainName = domainName;
            SecondLevelDomain = domainName.SecondLevelDomain;
            TopLevelDomain = domainName.TopLevelDomain.Value;

            ServiceId = serviceId;
            VerificationToken = verificationToken;

            var service = new GoogleSuiteService(serviceId, verificationToken);
            ServiceType = service.Type.Value;
            ServiceLabel = service.Label;
        }

        [JsonConstructor]
        private GoogleSuiteWasAdded(
            [JsonProperty("secondLevelDomain")] string secondLevelDomain,
            [JsonProperty("topLevelDomain")] string topLevelDomain,
            [JsonProperty("serviceId")] Guid serviceId,
            [JsonProperty("verificationToken")] string verificationToken)
            : this(
                new DomainName(
                    new SecondLevelDomain(secondLevelDomain),
                    Dns.TopLevelDomain.FromValue(topLevelDomain)),
                new ServiceId(serviceId),
                new GoogleVerificationToken(verificationToken)) { }
    }
}
