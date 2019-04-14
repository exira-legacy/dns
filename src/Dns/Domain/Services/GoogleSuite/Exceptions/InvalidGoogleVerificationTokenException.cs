namespace Dns.Domain.Services.GoogleSuite.Exceptions
{
    public class InvalidGoogleVerificationTokenException : DnsException
    {
        public InvalidGoogleVerificationTokenException(string message) : base(message) { }
    }

    public class EmptyGoogleVerificationTokenException : InvalidGoogleVerificationTokenException
    {
        public EmptyGoogleVerificationTokenException() : base("Token of a Google domain verification token cannot be empty.") { }
    }
}
