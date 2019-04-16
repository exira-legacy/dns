namespace Dns.Api.Infrastructure.LastObservedPosition
{
    using System;
    using System.Runtime.Serialization;
    using Swashbuckle.AspNetCore.Filters;

    [DataContract(Name = "LastObservedPosition", Namespace = "")]
    public class LastObservedPositionResponse
    {
        /// <summary>
        /// Last observed position of the store/projection.
        /// </summary>
        [DataMember(Name = "LastObservedPosition", Order = 1)]
        public long LastObservedPosition { get; set; }

        public LastObservedPositionResponse(
            long lastObservedPosition)
        {
            LastObservedPosition = lastObservedPosition;
        }
    }

    public class LastObservedPositionResponseExamples : IExamplesProvider
    {
        private static readonly Random Random = new Random();

        public object GetExamples() =>
            new LastObservedPositionResponse(Random.Next(1, 1337));
    }
}
