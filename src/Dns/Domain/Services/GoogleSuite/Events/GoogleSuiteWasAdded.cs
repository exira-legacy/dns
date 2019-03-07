namespace Dns.Domain.Services.GoogleSuite.Events
{
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Newtonsoft.Json;

    [EventName("GoogleSuiteWasAdded")]
    [EventDescription("The G Suite service was added.")]
    public class GoogleSuiteWasAdded
    {
        public string VerificationToken { get; }

        public GoogleSuiteWasAdded(
            GoogleVerificationToken verificationToken)
        {
            VerificationToken = verificationToken;
        }

        [JsonConstructor]
        private GoogleSuiteWasAdded(
            string verificationToken)
            : this(new GoogleVerificationToken(verificationToken)) { }
    }
}
