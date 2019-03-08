namespace Dns.Domain.Services.Manual
{
    public class ManualService : IService
    {
        private readonly ManualLabel _label;
        private readonly RecordSet _records;

        public ManualService(
            ManualLabel label,
            RecordSet records)
        {
            _label = label;
            _records = records;
        }

        public RecordSet GetRecords() => _records;
    }
}
