namespace Dns.Api.Infrastructure.Modules
{
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Be.Vlaanderen.Basisregisters.DataDog.Tracing.Autofac;
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Be.Vlaanderen.Basisregisters.EventHandling.Autofac;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Dns.Infrastructure.Modules;

    public class ApiModule : Module
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceCollection _services;

        public ApiModule(
            IConfiguration configuration,
            IServiceCollection services)
        {
            _configuration = configuration;
            _services = services;
        }

        protected override void Load(ContainerBuilder containerBuilder)
        {
            var eventSerializerSettings = EventsJsonSerializerSettingsProvider.CreateSerializerSettings();

            containerBuilder
                .RegisterModule(new LoggingModule(_configuration, _services))
                .RegisterModule(new DataDogModule(_configuration))
                .RegisterModule(new EventHandlingModule(typeof(DomainAssemblyMarker).Assembly, eventSerializerSettings))
                .RegisterModule(new CommandHandlingModule(_configuration));

            containerBuilder
                .Populate(_services);
        }
    }
}
