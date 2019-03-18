namespace Dns.Domain.Services.Manual
{
    public class ManualService : IService
    {
        private readonly ManualLabel _label;
        private readonly RecordSet _records;

        internal static string ServiceType = "manual";
        internal static string ServiceName = "Manual";

        public ServiceId ServiceId { get; }
        public ServiceType Type => Dns.ServiceType.manual;
        public ServiceLabel Label => new ServiceLabel(_label);

        public ManualService(
            ServiceId serviceId,
            ManualLabel label,
            RecordSet records)
        {
            ServiceId = serviceId;

            _label = label;
            _records = records;
        }

        public RecordSet GetRecords() => _records;
    }
}
