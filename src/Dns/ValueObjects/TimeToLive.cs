namespace Dns
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Exceptions;
    using Newtonsoft.Json;

    public class TimeToLive : IntegerValueObject<TimeToLive>
    {
        /// It is hereby specified that a TTL value is an unsigned number, with a minimum value of 0, and a maximum value of 2147483647.
        public const int MaxValue = int.MaxValue;

        public TimeToLive([JsonProperty("value")] int timeToLive) : base(timeToLive)
        {
            if (Value <= 0)
                throw new NegativeTimeToLiveException();
        }
    }
}
