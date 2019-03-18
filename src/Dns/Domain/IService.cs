namespace Dns.Domain
{
    public interface IService
    {
        ServiceId ServiceId { get; }

        ServiceType Type { get; }

        ServiceLabel Label { get; }

        RecordSet GetRecords();
    }
}
