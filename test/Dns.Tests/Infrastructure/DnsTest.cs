namespace Dns.Tests.Infrastructure
{
    using System.Collections.Generic;
    using Autofac;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing.Comparers;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing.SqlStreamStore.Autofac;
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Be.Vlaanderen.Basisregisters.EventHandling.Autofac;
    using Dns.Infrastructure.Modules;
    using KellermanSoftware.CompareNetObjects;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Xunit.Abstractions;

    public class DnsTest : AutofacBasedTest
    {
        private readonly JsonSerializerSettings _eventSerializerSettings = EventsJsonSerializerSettingsProvider.CreateSerializerSettings();
        private readonly IConfigurationRoot _configuration;

        public DnsTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var builder = new ConfigurationBuilder();

            builder.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:Events", "DummyConnection" },
            });

            _configuration = builder.Build();
        }

        protected override void ConfigureCommandHandling(ContainerBuilder builder)
            => builder.RegisterModule(new CommandHandlingModule(_configuration));

        protected override void ConfigureEventHandling(ContainerBuilder builder)
            => builder.RegisterModule(new EventHandlingModule(typeof(DomainAssemblyMarker).Assembly, _eventSerializerSettings));

        protected override IFactComparer CreateFactComparer()
        {
            var comparer = new CompareLogic();
            comparer.Config.MembersToIgnore.Add("Provenance");
            return new CompareNetObjectsBasedFactComparer(comparer);
        }
    }
}
