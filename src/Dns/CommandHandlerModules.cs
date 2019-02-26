namespace Dns
{
    using Autofac;
    using Be.Vlaanderen.Basisregisters.CommandHandling;
    using Domain;

    public static class CommandHandlerModules
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterType<DomainCommandHandlerModule>()
                .Named<CommandHandlerModule>(typeof(DomainCommandHandlerModule).FullName)
                .As<CommandHandlerModule>();
        }
    }
}
