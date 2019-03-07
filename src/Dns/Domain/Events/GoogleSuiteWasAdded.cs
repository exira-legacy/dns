namespace Dns.Domain.Events
{
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Newtonsoft.Json;

    [EventName("GoogleSuiteWasAdded")]
    [EventDescription("The G Suite service was added.")]
    public class GoogleSuiteWasAdded
    {
        public GoogleSuiteWasAdded()
        {
        }

        [JsonConstructor]
        private GoogleSuiteWasAdded(
            string secondLevelDomain,
            string topLevelDomain)
            : this() {}
    }
}
