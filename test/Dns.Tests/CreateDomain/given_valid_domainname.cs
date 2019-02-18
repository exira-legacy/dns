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
        public given_valid_domainname(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Theory, DefaultData]
        public void then_domain_should_have_been_created(
            CreateDomain createDomainCommand)
        {
            Assert(new Scenario()
                .GivenNone()
                .When(createDomainCommand)
                .Then(createDomainCommand.Name,
                    new DomainWasCreated(createDomainCommand.Name)));
        }
    }
}
