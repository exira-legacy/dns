namespace Dns.Tests
{
    using Exceptions;
    using Xunit;

    public class ServiceLabelTests
    {
        [Fact]
        public void label_cannot_be_empty()
        {
            void NullLabel() => new ServiceLabel(null);

            var ex = Record.Exception(NullLabel);

            Assert.NotNull(ex);
            Assert.IsType<EmptyServiceLabelException>(ex);
        }

        [Fact]
        public void label_cannot_be_too_long()
        {
            void LongLabel() => new ServiceLabel(new string('a', ServiceLabel.MaxLength + 1));

            var ex = Record.Exception(LongLabel);

            Assert.NotNull(ex);
            Assert.IsType<ServiceLabelTooLongException>(ex);
        }

        [Theory]
        [InlineData("Mijn Settings")]
        [InlineData("Office")]
        [InlineData("Home Servers")]
        public void label_must_be_valid(string label)
        {
            void ValidLabel() => new ServiceLabel(label);

            var ex = Record.Exception(ValidLabel);

            Assert.Null(ex);
        }
    }
}
