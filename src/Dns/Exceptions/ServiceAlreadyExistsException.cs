namespace Dns.Exceptions
{
    public class ServiceAlreadyExistsException : DnsException
    {
        public ServiceAlreadyExistsException(ServiceId serviceId) : base($"Service '{serviceId}' already exists, cannot be added twice.") { }
    }
}
