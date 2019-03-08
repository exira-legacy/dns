namespace Dns.Domain.Services.Manual
{
    public class ManualService : IService
    {
        private readonly ManualLabel _label;
        private readonly RecordSet _records;

        public ServiceId ServiceId { get; }

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
