namespace Dns.Tests
{
    using AutoFixture;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing;
    using Domain.Events;
    using Domain.Services.Manual;
    using Domain.Services.Manual.Commands;
    using Domain.Services.Manual.Events;
    using Exceptions;
    using Infrastructure;
    using Xunit;
    using Xunit.Abstractions;
    using DomainName = DomainName;
    using Record = Record;

    public class AddManualTests : DnsTest
    {
        public Fixture Fixture { get; }

        public AddManualTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            Fixture = new Fixture();
            Fixture.CustomizeSecondLevelDomain();
            Fixture.CustomizeTopLevelDomain();
            Fixture.CustomizeDomainName();
            Fixture.CustomizeManualLabel();
            Fixture.CustomizeRecordType();
            Fixture.CustomizeTimeToLive();
            Fixture.CustomizeRecordLabel();
            Fixture.CustomizeRecordValue();
            Fixture.CustomizeRecord();
        }

        [Fact]
        public void manual_should_be_added()
        {
            var domainName = Fixture.Create<DomainName>();
            var serviceId = Fixture.Create<ServiceId>();
            var label = Fixture.Create<ManualLabel>();
            var recordset = new RecordSet(Fixture.CreateMany<Record>(10));

            var manualService = new ManualService(serviceId, label, recordset);

            Assert(new Scenario()
                .Given(domainName, new DomainWasCreated(domainName))
                .When(new AddManual(domainName, serviceId, label, recordset))
                .Then(domainName,
                    new ManualWasAdded(domainName, serviceId, label, recordset),
                    new RecordSetUpdated(domainName, manualService.GetRecords())));
        }

        [Fact]
        public void adding_identical_manual_should_be_impossible()
        {
            var domainName = Fixture.Create<DomainName>();
            var serviceId = Fixture.Create<ServiceId>();
            var label = Fixture.Create<ManualLabel>();
            var recordset = new RecordSet(Fixture.CreateMany<Record>(10));

            Assert(new Scenario()
                .Given(domainName,
                    new DomainWasCreated(domainName),
                    new ManualWasAdded(domainName, serviceId, label, recordset))
                .When(new AddManual(domainName, serviceId, label, recordset))
                .Throws(new ServiceAlreadyExistsException(serviceId)));
        }
    }
}
