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
            txt = new RecordType("TXT", "TXT"),
            mx = new RecordType("MX", "MX"),
            cname = new RecordType("CNAME", "CNAME");

        private RecordType(
            string type,
            string displayName) : base(type, displayName) { }
    }
}
