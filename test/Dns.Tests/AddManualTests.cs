namespace Dns.Tests
{
    using AutoFixture;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing;
    using Domain.Events;
    using Domain.Services.Manual;
    using Domain.Services.Manual.Commands;
    using Domain.Services.Manual.Events;
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
            var label = Fixture.Create<ManualLabel>();
            var recordset = new RecordSet(Fixture.CreateMany<Record>(10));

            var manualService = new ManualService(label, recordset);

            Assert(new Scenario()
                .Given(domainName, new DomainWasCreated(domainName))
                .When(new AddManual(domainName, label, recordset))
                .Then(domainName,
                    new ManualWasAdded(label, recordset),
                    new RecordSetUpdated(manualService.GetRecords())));
        }
    }
}
