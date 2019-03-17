namespace Dns.Tests
{
    using Exceptions;
    using Xunit;

    public class RecordValueTests
    {
        [Fact]
        public void value_cannot_be_empty()
        {
            void NullValue() => new RecordValue(null);

            var ex = Record.Exception(NullValue);

            Assert.NotNull(ex);
            Assert.IsType<EmptyRecordValueException>(ex);
        }

        [Fact]
        public void value_cannot_be_too_long()
        {
            void LongValue() => new RecordValue(new string('a', RecordValue.MaxLength + 1));

            var ex = Record.Exception(LongValue);

            Assert.NotNull(ex);
            Assert.IsType<RecordValueTooLongException>(ex);
        }

        [Theory]
        [InlineData("10 aspmx.l.google.com.")]
        [InlineData("e6363c95fb8f4ec8febf25398c7fe069e5836ffc.comodoca.com.")]
        [InlineData("10 5d7ae46fe28a8947ba34c21a385dce.pamx1.hotmail.com.")]
        [InlineData("10.0.0.7")]
        [InlineData("google-site-verification=gn9B2LMgMSL20TH2RIHf3lLscp2unRMFkoSpQ5qwC58")]
        [InlineData("keybase-site-verification=llhHFoXIFGOzl8Boebxt5DTjY7HsMW2fZHY2EroVNaU")]
        [InlineData("cumpsd.github.io.")]
        public void value_must_be_valid(string label)
        {
            void ValidValue() => new RecordValue(label);

            var ex = Record.Exception(ValidValue);

            Assert.Null(ex);
        }
    }
}
