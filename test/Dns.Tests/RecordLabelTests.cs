namespace Dns.Tests
{
    using System;
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

        [Fact]
        public void label_cannot_be_too_long()
        {
            void LongLabel() => new RecordLabel(new string('a', RecordLabel.MaxLength + 1));

            var ex = Record.Exception(LongLabel);

            Assert.NotNull(ex);
            Assert.IsType<RecordLabelTooLongException>(ex);
        }

        [Fact]
        public void label_cannot_start_with_a_dash()
        {
            void StartDashLabel() => new RecordLabel("-blabla");

            var ex = Record.Exception(StartDashLabel);

            Assert.NotNull(ex);
            Assert.IsType<AggregateException>(ex);

            Assert.NotNull(ex.InnerException);
            Assert.IsType<RecordLabelCannotStartWithDashException>(ex.InnerException);
        }

        [Fact]
        public void label_cannot_end_with_a_dash()
        {
            void EndDashLabel() => new RecordLabel("blabla-");

            var ex = Record.Exception(EndDashLabel);

            Assert.NotNull(ex);
            Assert.IsType<AggregateException>(ex);

            Assert.NotNull(ex.InnerException);
            Assert.IsType<RecordLabelCannotEndWithDashException>(ex.InnerException);
        }

        [Fact]
        public void label_cannot_be_only_digits()
        {
            void DigitsLabel() => new RecordLabel("1232345346457");

            var ex = Record.Exception(DigitsLabel);

            Assert.NotNull(ex);
            Assert.IsType<AggregateException>(ex);

            Assert.NotNull(ex.InnerException);
            Assert.IsType<RecordLabelCannotBeAllDigitsException>(ex.InnerException);
        }

        [Fact]
        public void label_cannot_have_invalid_characters()
        {
            void InvalidLabel() => new RecordLabel("bla bla");

            var ex = Record.Exception(InvalidLabel);

            Assert.NotNull(ex);
            Assert.IsType<AggregateException>(ex);

            Assert.NotNull(ex.InnerException);
            Assert.IsType<RecordLabelContainsInvalidCharactersException>(ex.InnerException);
        }

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
