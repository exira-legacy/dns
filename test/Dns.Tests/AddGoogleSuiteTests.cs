namespace Dns.Tests
{
    using AutoFixture;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing;
    using Domain.Events;
    using Domain.Services.GoogleSuite;
    using Domain.Services.GoogleSuite.Commands;
    using Domain.Services.GoogleSuite.Events;
    using Exceptions;
    using Infrastructure;
    using Xunit;
    using Xunit.Abstractions;
    using DomainName = DomainName;

    public class AddGoogleSuiteTests : DnsTest
    {
        public Fixture Fixture { get; }

        public AddGoogleSuiteTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            Fixture = new Fixture();
            Fixture.CustomizeSecondLevelDomain();
            Fixture.CustomizeTopLevelDomain();
            Fixture.CustomizeDomainName();
            Fixture.CustomizeGoogleVerificationToken();
        }

        [Fact]
        public void google_suite_should_be_added()
        {
            var domainName = Fixture.Create<DomainName>();
            var serviceId = Fixture.Create<ServiceId>();
            var verificationToken = Fixture.Create<GoogleVerificationToken>();

            var googleService = new GoogleSuiteService(serviceId, verificationToken);

            Assert(new Scenario()
                .Given(domainName, new DomainWasCreated(domainName))
                .When(new AddGoogleSuite(domainName, serviceId, verificationToken))
                .Then(domainName,
                    new GoogleSuiteWasAdded(domainName, serviceId, verificationToken),
                    new RecordSetUpdated(domainName, googleService.GetRecords())));
        }

        [Fact]
        public void adding_identical_google_suite_should_be_impossible()
        {
            var domainName = Fixture.Create<DomainName>();
            var serviceId = Fixture.Create<ServiceId>();
            var verificationToken = Fixture.Create<GoogleVerificationToken>();

            Assert(new Scenario()
                .Given(domainName,
                    new DomainWasCreated(domainName),
                    new GoogleSuiteWasAdded(domainName, serviceId, verificationToken))
                .When(new AddGoogleSuite(domainName, serviceId, verificationToken))
                .Throws(new ServiceAlreadyExistsException(serviceId)));
        }
    }
}
