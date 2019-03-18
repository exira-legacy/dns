namespace Dns.Domain
{
    public interface IService
    {
        ServiceId ServiceId { get; }

        // TODO: Add to make life easier
        //ServiceType Type { get; set; }

        //ServiceLabel Label { get; set; }

        RecordSet GetRecords();
    }
}
