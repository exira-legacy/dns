namespace Dns.Exceptions
{
    public class InvalidRecordException : DnsException
    {
        public InvalidRecordException(string message) : base(message) { }
    }

    public class InvalidRecordLabelException : InvalidRecordException
    {
        public InvalidRecordLabelException(string message) : base(message) { }
    }

    public class InvalidRecordValueException : InvalidRecordException
    {
        public InvalidRecordValueException(string message) : base(message) { }
    }

    public class InvalidTimeToLiveException : InvalidRecordException
    {
        public InvalidTimeToLiveException(string message) : base(message) { }
    }

    public class EmptyRecordLabelException : InvalidRecordLabelException
    {
        public EmptyRecordLabelException() : base("Label of a record cannot be empty.") { }
    }

    public class EmptyRecordValueException : InvalidRecordValueException
    {
        public EmptyRecordValueException() : base("Value of a record cannot be empty.") { }
    }

    public class NegativeTimeToLiveException : InvalidTimeToLiveException
    {
        public NegativeTimeToLiveException() : base("Time to live cannot be negative.") { }
    }
}
