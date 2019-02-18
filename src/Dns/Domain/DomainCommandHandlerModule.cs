namespace Dns.Domain
{
    using System;
    using Be.Vlaanderen.Basisregisters.CommandHandling;
    using Commands;

    public sealed class DomainCommandHandlerModule : CommandHandlerModule
    {
        public DomainCommandHandlerModule(
            Func<IDomains> getDomains,
            ReturnHandler<CommandMessage> finalHandler = null) : base(finalHandler)
        {
            For<CreateDomain>()
                .Handle(async (message, ct) =>
                {
                    var domains = getDomains();

                    var domainName = message.Command.Name;
                    var domain = Domain.Register(domainName);

                    domains.Add(domainName, domain);
                });
        }
    }
}
