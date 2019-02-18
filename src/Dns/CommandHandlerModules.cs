namespace Dns
{
    using System;
    using Be.Vlaanderen.Basisregisters.CommandHandling.SqlStreamStore.Autofac;
    using Autofac;
    using Domain;

    public static class CommandHandlerModules
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterSqlStreamStoreCommandHandler<DomainCommandHandlerModule>(
                    c => handler =>
                        new DomainCommandHandlerModule(
                            c.Resolve<Func<IDomains>>(),
                            handler));
        }
    }
}
