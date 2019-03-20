namespace Dns.Domain.Services.Manual
{
    using Newtonsoft.Json;

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

        public string GetServiceData() => JsonConvert.SerializeObject(new ServiceData(this));

        public class ServiceData
        {
            public ManualLabel Label { get; set; }
            public RecordSet Records { get; set; }

            public ServiceData(ManualService service)
            {
                Label = service._label;
                Records = service._records;
            }
        }
    }
}
