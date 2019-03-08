namespace Dns.Domain.Services.Manual.Commands
{
    public class AddManual
    {
        public DomainName DomainName { get; }

        public ServiceId ServiceId { get; }

        public ManualLabel Label { get; }

        public RecordSet Records { get; }

        public AddManual(
            DomainName domainName,
            ServiceId serviceId,
            ManualLabel label,
            RecordSet records)
        {
            DomainName = domainName;
            ServiceId = serviceId;
            Label = label;
            Records = records;
        }
    }
}
