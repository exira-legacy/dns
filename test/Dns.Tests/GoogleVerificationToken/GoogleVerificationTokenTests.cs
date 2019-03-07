namespace Dns.Tests
{
    using Domain.Services.GoogleSuite;
    using Xunit;

    public class GoogleVerificationTokenTests
    {
        [Fact]
        public void token_cannot_be_empty()
        {
            void NullToken() => new GoogleVerificationToken(null);

            var ex = Record.Exception(NullToken);

            Assert.NotNull(ex);
            Assert.IsType<NoNameException>(ex);
        }

        [Theory]
        [InlineData("rXOxyZounnZasA8Z7oaD3c14JdjS9aKSWvsR1EbUSIQ")]
        public void token_must_be_valid(string token)
        {
            void ValidToken() => new GoogleVerificationToken(token);

            var ex = Record.Exception(ValidToken);

            Assert.Null(ex);
        }
    }
}
