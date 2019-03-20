namespace Dns.Domain.Services.GoogleSuite.Exceptions
{
    public class GoogleSuiteServiceAlreadyExistsException : DnsException
    {
        public GoogleSuiteServiceAlreadyExistsException() : base("Google Suite has already been added.") { }
    }
}
