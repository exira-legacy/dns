namespace Dns.Tests
{
    using Exceptions;
    using Xunit;

    public class RecordValueTests
    {
        [Fact]
        public void label_cannot_be_empty()
        {
            void NullValue() => new RecordValue(null);

            var ex = Record.Exception(NullValue);

            Assert.NotNull(ex);
            Assert.IsType<EmptyRecordValueException>(ex);
        }

        // TODO: Add test for invalid labels

        [Theory]
        [InlineData("10 aspmx.l.google.com.")]
        [InlineData("10.0.0.7")]
        [InlineData("google-site-verification=gn9B2LMgMSL20TH2RIHf3lLscp2unRMFkoSpQ5qwC58")]
        [InlineData("keybase-site-verification=llhHFoXIFGOzl8Boebxt5DTjY7HsMW2fZHY2EroVNaU")]
        [InlineData("cumpsd.github.io.")]
        public void label_must_be_valid(string label)
        {
            void ValidValue() => new RecordValue(label);

            var ex = Record.Exception(ValidValue);

            Assert.Null(ex);
        }
    }
}
