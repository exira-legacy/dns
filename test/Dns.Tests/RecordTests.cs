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

        [Theory]
        [InlineData("A", 3600, "a.a", "bla")]
        [InlineData("A", 3600, "a_a", "bla")]
        [InlineData("TXT", 3600, "a!a", "bla")]
        public void label_cannot_be_invalid(string recordType, int timeToLive, string recordLabel, string recordValue)
        {
            void InvalidRecord() => new Dns.Record(
                RecordType.FromValue(recordType.ToLowerInvariant()),
                new TimeToLive(timeToLive),
                new RecordLabel(recordLabel),
                new RecordValue(recordValue));

            var ex = Record.Exception(InvalidRecord);

            Assert.NotNull(ex);
            Assert.IsType<InvalidRecordLabelException>(ex);
        }

        [Theory]
        [InlineData("A", 3600, "www", "bla", typeof(RecordValueARecordMustBeValidIpException))]
        [InlineData("CNAME", 3600, "www", "-blabla.", typeof(RecordValueCNameRecordInvalidHostnameException))]
        [InlineData("CNAME", 3600, "www", "-www-", typeof(RecordValueCNameRecordInvalidLabelException))]
        [InlineData("MX", 3600, "www", "w www", typeof(RecordValueMxRecordMustHaveIntegerPriorityException))]
        [InlineData("MX", 3600, "www", "-10 www", typeof(RecordValueMxRecordMustHaveIntegerPriorityException))]
        [InlineData("MX", 3600, "www", "10", typeof(RecordValueMxRecordMustHavePriorityAndHostnameException))]
        [InlineData("MX", 3600, "www", "10 -blablabla.", typeof(RecordValueMxRecordInvalidHostnameException))]
        [InlineData("MX", 3600, "www", "10 -www-", typeof(RecordValueMxRecordInvalidLabelException))]
        [InlineData("NS", 3600, "www", "-blablabla.", typeof(RecordValueNsRecordInvalidHostnameException))]
        [InlineData("NS", 3600, "www", "-www-", typeof(RecordValueNsRecordInvalidLabelException))]
        public void value_cannot_be_invalid(string recordType, int timeToLive, string recordLabel, string recordValue, Type exceptionType)
        {
            void InvalidRecord() => new Dns.Record(
                RecordType.FromValue(recordType.ToLowerInvariant()),
                new TimeToLive(timeToLive),
                new RecordLabel(recordLabel),
                new RecordValue(recordValue));

            var ex = Record.Exception(InvalidRecord);

            Assert.NotNull(ex);
            Assert.IsType<InvalidRecordValueException>(ex);

            Assert.NotNull(ex.InnerException);
            Assert.IsType<AggregateException>(ex.InnerException);
            Assert.IsType(exceptionType, ex.InnerException.InnerException);
        }

        [Theory]
        [InlineData("A", 3600, "@", "127.0.0.1")]
        [InlineData("CNAME", 3600, "www", "exira.com.")]
        [InlineData("TXT", 3600, "@", "blablabla")]
        [InlineData("TXT", 3600, "_bla", "blablabla")]
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
