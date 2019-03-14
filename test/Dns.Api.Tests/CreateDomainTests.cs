namespace Dns.Api.Tests
{
    using System.Threading.Tasks;
    using Dns.Domain.Commands;
    using Domain.Requests;
    using Infrastructure;
    using Xunit;
    using Xunit.Abstractions;

    public class CreateDomainTests : ApiTest
    {
        public CreateDomainTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public async Task Test()
        {
            var request = new CreateDomainRequest
            {
                SecondLevelDomain = "exira",
                TopLevelDomain = "com"
            };

            var commands = await Post("/v1/domains", request);

            Assert.True(commands.Count == 1);

            commands[0].IsEqual(
                new CreateDomain(
                    new DomainName(
                        new SecondLevelDomain(request.SecondLevelDomain),
                        TopLevelDomain.FromValue(request.TopLevelDomain))));
        }
    }
}
