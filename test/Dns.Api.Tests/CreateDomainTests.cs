namespace Dns.Api.Tests
{
    using System.Threading.Tasks;
    using Dns.Domain.Commands;
    using Domain.Requests;
    using FluentValidation.TestHelper;
    using Infrastructure;
    using Xunit;
    using Xunit.Abstractions;

    public class CreateDomainTests : ApiTest
    {
        public CreateDomainTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public void should_validate_request()
        {
            var validator = new CreateDomainRequestValidator();

            validator.ShouldHaveValidationErrorFor(x => x.TopLevelDomain, null as string);
            validator.ShouldHaveValidationErrorFor(x => x.TopLevelDomain, "bla");

            validator.ShouldHaveValidationErrorFor(x => x.SecondLevelDomain, null as string);
            validator.ShouldHaveValidationErrorFor(x => x.SecondLevelDomain, "bla bla");
            validator.ShouldHaveValidationErrorFor(x => x.SecondLevelDomain, new string('a', SecondLevelDomain.MaxLength + 10));

            var validRequest = new CreateDomainRequest
            {
                SecondLevelDomain = "exira",
                TopLevelDomain = "com"
            };

            validator.ShouldNotHaveValidationErrorFor(x => x.TopLevelDomain, validRequest);
            validator.ShouldNotHaveValidationErrorFor(x => x.SecondLevelDomain, validRequest);
        }

        [Fact]
        public async Task should_create_a_correct_command()
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

        [Fact]
        public async Task should_fail_on_invalid_data()
        {
            var request = new CreateDomainRequest
            {
                SecondLevelDomain = "ex ira",
                TopLevelDomain = "com-gibberish"
            };

            var commands = await Post("/v1/domains", request);

            Assert.True(commands.Count == 0);
        }
    }
}
