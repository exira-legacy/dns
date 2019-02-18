namespace Dns.Tests.CreateDomain
{
    using AutoFixture;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing;
    using Domain.Commands;
    using Domain.Events;
    using Xunit;
    using Xunit.Abstractions;

    public class given_valid_domainname : DnsTest
    {
        public Fixture Fixture { get; }

        public given_valid_domainname(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            Fixture = new Fixture();
            Fixture.CustomizeSecondLevelDomain();
            Fixture.CustomizeTopLevelDomain();
        }

        [Fact]
        public void then_domain_should_have_been_created()
        {
            var createDomainCommand = Fixture.Create<CreateDomain>();

            Assert(new Scenario()
                .GivenNone()
                .When(createDomainCommand)
                .Then(createDomainCommand.DomainName,
                    new DomainWasCreated(createDomainCommand.DomainName)));
        }
    }
}
