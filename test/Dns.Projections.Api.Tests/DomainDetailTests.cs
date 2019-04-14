namespace Dns.Projections.Api.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoFixture;
    using Domain.Events;
    using DomainDetail;
    using Infrastructure;
    using Xunit;
    using Xunit.Abstractions;
    using Dns.Tests.Infrastructure;

    public class DomainDetailTests : ProjectionsTest
    {
        public Fixture Fixture { get; }

        public DomainDetailTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            Fixture = new Fixture();
            Fixture.CustomizeSecondLevelDomain();
            Fixture.CustomizeTopLevelDomain();
            Fixture.CustomizeDomainName();
        }

        [Fact]
        public Task when_domain_was_created()
        {
            var data = Fixture
                .CreateMany<DomainWasCreated>(new Random().Next(1, 100))
                .Select(@event =>
                {
                    var expectedRecord = new DomainDetail
                    {
                        Name = @event.DomainName,
                        SecondLevelDomain = @event.SecondLevelDomain,
                        TopLevelDomain = @event.TopLevelDomain
                    };

                    return new
                    {
                        DomainWasCreated = @event,
                        ExpectedRecord = expectedRecord
                    };
                }).ToList();

            return new DomainDetailProjections()
                .Scenario()
                .Given(data.Select(d => d.DomainWasCreated))
                .Expect(TestOutputHelper, data.Select(d => d.ExpectedRecord));
        }
    }
}
