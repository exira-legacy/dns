namespace Dns.Tests
{
    using Domain.Services.Manual;
    using Domain.Services.Manual.Exceptions;
    using Xunit;

    public class ManualLabelTests
    {
        [Fact]
        public void label_cannot_be_empty()
        {
            void NullLabel() => new ManualLabel(null);

            var ex = Record.Exception(NullLabel);

            Assert.NotNull(ex);
            Assert.IsType<EmptyManualLabelException>(ex);
        }

        [Theory]
        [InlineData("Mijn Settings")]
        [InlineData("Office")]
        [InlineData("Home Servers")]
        public void label_must_be_valid(string label)
        {
            void ValidLabel() => new ManualLabel(label);

            var ex = Record.Exception(ValidLabel);

            Assert.Null(ex);
        }
    }
}
