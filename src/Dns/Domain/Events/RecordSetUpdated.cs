namespace Dns.Domain.Events
{
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Newtonsoft.Json;

    [EventName("RecordSetUpdated")]
    [EventDescription("The complete record set was updated.")]
    public class RecordSetUpdated
    {
        public RecordSetUpdated()
        {
        }

        [JsonConstructor]
        private RecordSetUpdated(
            string secondLevelDomain,
            string topLevelDomain)
            : this() {}
    }
}
