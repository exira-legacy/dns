namespace Dns
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Exceptions;
    using Newtonsoft.Json;

    public class TimeToLive : IntegerValueObject<TimeToLive>
    {
        public const int MaxValue = int.MaxValue; // TODO: Look up what the max value is

        public TimeToLive([JsonProperty("value")] int timeToLive) : base(timeToLive)
        {
            if (timeToLive <= 0)
                throw new NegativeTimeToLiveException();
        }
    }
}
