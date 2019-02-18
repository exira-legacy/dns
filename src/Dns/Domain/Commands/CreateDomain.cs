namespace Dns.Domain.Commands
{
    public class CreateDomain
    {
        public DomainName DomainName { get; }

        public CreateDomain(DomainName domainName) => DomainName = domainName;
    }
}
