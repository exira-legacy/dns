namespace Dns.Tests
{
    using System;
    using Xunit;

    public class DomainNameTests
    {
        [Fact]
        public void secondleveldomain_cannot_be_empty()
        {
            void NullSecondLevelDomain() => new DomainName(null, null);

            var ex = Record.Exception(NullSecondLevelDomain);

            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void topleveldomain_cannot_be_empty()
        {
            void NullTopLevelDomain() => new DomainName(new SecondLevelDomain("bla"), null);

            var ex = Record.Exception(NullTopLevelDomain);

            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Theory]
        [InlineData("exira", "com")]
        [InlineData("cumps", "be")]
        public void domain_must_be_valid(string secondLevelDomain, string topLevelDomain)
        {
            void ValidDomain() => new DomainName(new SecondLevelDomain(secondLevelDomain), TopLevelDomain.FromValue(topLevelDomain));

            var ex = Record.Exception(ValidDomain);

            Assert.Null(ex);
        }
    }
}
