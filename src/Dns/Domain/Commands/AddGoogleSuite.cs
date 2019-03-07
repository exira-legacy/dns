namespace Dns.Domain.Commands
{
    public class AddGoogleSuite
    {
        public DomainName DomainName { get; }

        public AddGoogleSuite(DomainName domainName) => DomainName = domainName;
    }
}
