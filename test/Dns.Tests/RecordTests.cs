namespace Dns.Tests
{
    using System;
    using Exceptions;
    using Xunit;

    public class RecordTests
    {
        [Fact]
        public void recordtype_cannot_be_empty()
        {
            void NullRecordType() => new Dns.Record(null, null, null, null);

            var ex = Record.Exception(NullRecordType);

            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Contains(nameof(Dns.Record.Type).ToLowerInvariant(), ex.Message.ToLowerInvariant());
        }

        [Fact]
        public void timetolive_cannot_be_empty()
        {
            void NullTimeToLive() => new Dns.Record(RecordType.a, null, null, null);

            var ex = Record.Exception(NullTimeToLive);

            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Contains(nameof(Dns.Record.TimeToLive).ToLowerInvariant(), ex.Message.ToLowerInvariant());
        }

        [Fact]
        public void label_cannot_be_empty()
        {
            void NullLabel() => new Dns.Record(RecordType.a, new TimeToLive(3600), null, null);

            var ex = Record.Exception(NullLabel);

            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Contains(nameof(Dns.Record.Label).ToLowerInvariant(), ex.Message.ToLowerInvariant());
        }

        [Fact]
        public void value_cannot_be_empty()
        {
            void NullValue() => new Dns.Record(RecordType.a, new TimeToLive(3600), new RecordLabel("@"), null);

            var ex = Record.Exception(NullValue);

            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Contains(nameof(Dns.Record.Value).ToLowerInvariant(), ex.Message.ToLowerInvariant());
        }

        [Fact]
        public void label_cannot_be_invalid()
        {
            void InvalidRecord() => new Dns.Record(RecordType.a, new TimeToLive(3600), new RecordLabel("a.a"), new RecordValue("bla"));

            var ex = Record.Exception(InvalidRecord);

            Assert.NotNull(ex);
            Assert.IsType<InvalidRecordLabelException>(ex);
        }

        [Theory]
        [InlineData("A", 3600, "@", "127.0.0.1")]
        [InlineData("CNAME", 3600, "www", "exira.com.")]
        [InlineData("TXT", 3600, "@", "blablabla")]
        [InlineData("spf", 9000, "@", "blabla bla")]
        public void record_must_be_valid(string recordType, int timeToLive, string recordLabel, string recordValue)
        {
            void ValidRecord() => new Dns.Record(
                RecordType.FromValue(recordType.ToLowerInvariant()),
                new TimeToLive(timeToLive),
                new RecordLabel(recordLabel),
                new RecordValue(recordValue));

            var ex = Record.Exception(ValidRecord);

            Assert.Null(ex);
        }
    }
}
