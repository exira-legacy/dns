namespace Dns.Tests
{
    using AutoFixture;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing;
    using Domain.Commands;
    using Domain.Events;
    using Infrastructure;
    using SqlStreamStore.Streams;
    using Xunit;
    using Xunit.Abstractions;
    using DomainName = DomainName;

    public class CreateDomainTests : DnsTest
    {
        public Fixture Fixture { get; }

        public CreateDomainTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            Fixture = new Fixture();
            Fixture.CustomizeSecondLevelDomain();
            Fixture.CustomizeTopLevelDomain();
            Fixture.CustomizeDomainName();
        }

        [Fact]
        public void domain_should_have_been_created()
        {
            var createDomainCommand = Fixture.Create<CreateDomain>();

            Assert(new Scenario()
                .GivenNone()
                .When(createDomainCommand)
                .Then(createDomainCommand.DomainName,
                    new DomainWasCreated(createDomainCommand.DomainName)));
        }

        [Fact]
        public void domain_should_not_be_duplicated()
        {
            var domainName = Fixture.Create<DomainName>();
            var createDomainCommand = new CreateDomain(domainName);

            Assert(new Scenario()
                .Given(domainName,
                    new DomainWasCreated(domainName))
                .When(createDomainCommand)
                .Throws(new WrongExpectedVersionException($"Append failed due to WrongExpectedVersion.Stream: {domainName}, Expected version: -1")));
        }
    }
}
