namespace Dns
{
    using System.Diagnostics.CodeAnalysis;

    public class InvalidRecordTypeException : EnumerationException
    {
        public InvalidRecordTypeException(string message, string paramName, object value, string type) :
            base(message, paramName, value, type) { }
    }

    public class RecordType : Enumeration<RecordType, string, InvalidRecordTypeException>
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static readonly RecordType
            ns = new RecordType("ns", "NS"),
            a = new RecordType("a", "A"),
            cname = new RecordType("cname", "CNAME"),
            mx = new RecordType("mx", "MX"),
            txt = new RecordType("txt", "TXT"),
            spf = new RecordType("spf", "SPF");

        private RecordType(
            string type,
            string displayName) : base(type?.ToLowerInvariant(), displayName) { }
    }
}
