namespace Dns.Domain
{
    public interface IService
    {
        ServiceId ServiceId { get; }

        RecordSet GetRecords();
    }
}
