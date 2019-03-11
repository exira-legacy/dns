namespace Dns.Tests
{
    using Exceptions;
    using Xunit;

    public class RecordLabelTests
    {
        [Fact]
        public void label_cannot_be_empty()
        {
            void NullLabel() => new RecordLabel(null);

            var ex = Record.Exception(NullLabel);

            Assert.NotNull(ex);
            Assert.IsType<EmptyRecordLabelException>(ex);
        }

        // TODO: Add test for invalid labels

        [Theory]
        [InlineData("@")]
        [InlineData("www")]
        [InlineData("ftp")]
        public void label_must_be_valid(string label)
        {
            void ValidLabel() => new RecordLabel(label);

            var ex = Record.Exception(ValidLabel);

            Assert.Null(ex);
        }
    }
}
