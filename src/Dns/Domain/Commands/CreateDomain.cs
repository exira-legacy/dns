namespace Dns.Domain.Commands
{
    public class CreateDomain
    {
        public DomainName Name { get; }

        public CreateDomain(DomainName name) => Name = name;
    }
}
