namespace Dns.Tests.CreateDomain
{
    using AutoFixture;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing;
    using Domain.Commands;
    using Domain.Events;
    using Xunit;
    using Xunit.Abstractions;

    public class CreateDomainTests : DnsTest
    {
        public Fixture Fixture { get; }

        public CreateDomainTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            Fixture = new Fixture();
            Fixture.CustomizeSecondLevelDomain();
            Fixture.CustomizeTopLevelDomain();
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
    }
}
