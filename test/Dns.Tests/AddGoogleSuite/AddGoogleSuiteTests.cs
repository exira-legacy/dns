namespace Dns.Tests.AddGoogleSuite
{
    using AutoFixture;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing;
    using Domain.Commands;
    using Domain.Events;
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
        }

        [Fact]
        public void google_suite_should_be_added()
        {
            var domainName = Fixture.Create<DomainName>();

            Assert(new Scenario()
                .Given(domainName, new DomainWasCreated(domainName))
                .When(new AddGoogleSuite(domainName))
                .Then(domainName,
                    new GoogleSuiteWasAdded(),
                    new RecordSetUpdated()));
        }
    }
}
