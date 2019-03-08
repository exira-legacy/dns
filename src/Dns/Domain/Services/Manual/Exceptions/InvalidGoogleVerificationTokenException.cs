namespace Dns.Domain.Services.Manual.Exceptions
{
    public class InvalidManualLabelException : DnsException
    {
        public InvalidManualLabelException(string message) : base(message) { }
    }

    public class EmptyManualLabelException : InvalidManualLabelException
    {
        public EmptyManualLabelException() : base("Label of a manual service cannot be empty.") {}
    }
}
