namespace Dns.Domain.Services.GoogleSuite.Events
{
    using System;
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Newtonsoft.Json;

    [EventName("GoogleSuiteWasAdded")]
    [EventDescription("The G Suite service was added.")]
    public class GoogleSuiteWasAdded
    {
        public Guid ServiceId { get; }
        public string VerificationToken { get; }

        public GoogleSuiteWasAdded(
            ServiceId serviceId,
            GoogleVerificationToken verificationToken)
        {
            ServiceId = serviceId;
            VerificationToken = verificationToken;
        }

        [JsonConstructor]
        private GoogleSuiteWasAdded(
            Guid serviceId,
            string verificationToken)
            : this(
                new ServiceId(serviceId),
                new GoogleVerificationToken(verificationToken)) { }
    }
}
