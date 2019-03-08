namespace Dns.Domain.Commands
{
    public class RemoveService
    {
        public DomainName DomainName { get; }

        public ServiceId ServiceId { get; }

        public RemoveService(
            DomainName domainName,
            ServiceId serviceId)
        {
            DomainName = domainName;
            ServiceId = serviceId;
        }
    }
}
