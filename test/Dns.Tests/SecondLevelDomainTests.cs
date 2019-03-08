namespace Dns.Tests
{
    using Exceptions;
    using Xunit;

    public class SecondLevelDomainTests
    {
        [Fact]
        public void domain_cannot_be_empty()
        {
            void NullDomain() => new SecondLevelDomain(null);

            var ex = Record.Exception(NullDomain);

            Assert.NotNull(ex);
            Assert.IsType<EmptySecondLevelDomainException>(ex);
        }

        [Fact]
        public void domain_cannot_be_over_maxlength()
        {
            void TooLongDomain() => new SecondLevelDomain(new string('a', SecondLevelDomain.MaxLength + 10));

            var ex = Record.Exception(TooLongDomain);

            Assert.NotNull(ex);
            Assert.IsType<InvalidHostnameException>(ex);
        }

        [Theory]
        [InlineData("exira")]
        [InlineData("chérie")]
        [InlineData("xn--xr-kia7a5a")]
        public void domain_must_be_valid(string name)
        {
            void ValidDomain() => new SecondLevelDomain(name);

            var ex = Record.Exception(ValidDomain);

            Assert.Null(ex);
        }
    }
}
