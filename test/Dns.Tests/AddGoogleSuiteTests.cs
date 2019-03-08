namespace Dns.Tests
{
    using AutoFixture;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing;
    using Domain.Events;
    using Domain.Services.GoogleSuite;
    using Domain.Services.GoogleSuite.Commands;
    using Domain.Services.GoogleSuite.Events;
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
            var verificationToken = Fixture.Create<GoogleVerificationToken>();

            Assert(new Scenario()
                .Given(domainName, new DomainWasCreated(domainName))
                .When(new AddGoogleSuite(domainName, verificationToken))
                .Then(domainName,
                    new GoogleSuiteWasAdded(verificationToken),
                    new RecordSetUpdated(new GoogleSuiteService(verificationToken).GetRecords())));
        }
    }
}
