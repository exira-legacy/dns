namespace Dns.Tests
{
    using AutoFixture;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing;
    using Domain.Commands;
    using Domain.Events;
    using Domain.Services.GoogleSuite;
    using Domain.Services.GoogleSuite.Events;
    using Infrastructure;
    using Xunit;
    using Xunit.Abstractions;
    using DomainName = DomainName;

    public class RemoveServiceTests : DnsTest
    {
        public Fixture Fixture { get; }

        public RemoveServiceTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            Fixture = new Fixture();
            Fixture.CustomizeSecondLevelDomain();
            Fixture.CustomizeTopLevelDomain();
            Fixture.CustomizeDomainName();
            Fixture.CustomizeGoogleVerificationToken();
        }

        [Fact]
        public void service_should_be_removed()
        {
            var domainName = Fixture.Create<DomainName>();
            var serviceId = Fixture.Create<ServiceId>();
            var verificationToken = Fixture.Create<GoogleVerificationToken>();

            Assert(new Scenario()
                .Given(domainName,
                    new DomainWasCreated(domainName),
                    new GoogleSuiteWasAdded(domainName, serviceId, verificationToken))
                .When(new RemoveService(domainName, serviceId))
                .Then(domainName,
                    new ServiceWasRemoved(domainName, serviceId),
                    new RecordSetWasUpdated(domainName, new RecordSet())));
        }

        [Fact]
        public void service_should_be_removed_from_multiple()
        {
            var domainName = Fixture.Create<DomainName>();
            var serviceId1 = Fixture.Create<ServiceId>();
            var serviceId2 = Fixture.Create<ServiceId>();
            var verificationToken = Fixture.Create<GoogleVerificationToken>();

            var googleService = new GoogleSuiteService(serviceId1, verificationToken);

            Assert(new Scenario()
                .Given(domainName,
                    new DomainWasCreated(domainName),
                    new GoogleSuiteWasAdded(domainName, serviceId1, verificationToken),
                    new GoogleSuiteWasAdded(domainName, serviceId2, verificationToken))
                .When(new RemoveService(domainName, serviceId2))
                .Then(domainName,
                    new ServiceWasRemoved(domainName, serviceId2),
                    new RecordSetWasUpdated(domainName, googleService.GetRecords())));
        }

        [Fact]
        public void removing_unknown_service_doesnt_matter()
        {
            var domainName = Fixture.Create<DomainName>();
            var serviceId1 = Fixture.Create<ServiceId>();
            var serviceId2 = Fixture.Create<ServiceId>();
            var verificationToken = Fixture.Create<GoogleVerificationToken>();

            Assert(new Scenario()
                .Given(domainName,
                    new DomainWasCreated(domainName),
                    new GoogleSuiteWasAdded(domainName, serviceId1, verificationToken))
                .When(new RemoveService(domainName, serviceId2))
                .ThenNone());
        }
    }
}
