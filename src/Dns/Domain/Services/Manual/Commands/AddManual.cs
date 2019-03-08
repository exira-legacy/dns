namespace Dns.Domain.Services.Manual.Commands
{
    public class AddManual
    {
        public DomainName DomainName { get; }

        public ManualLabel Label { get; }

        public RecordSet Records { get; }

        public AddManual(
            DomainName domainName,
            ManualLabel label,
            RecordSet records)
        {
            DomainName = domainName;
            Label = label;
            Records = records;
        }
    }
}
