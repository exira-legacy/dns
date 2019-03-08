namespace Dns.Exceptions
{
    public class InvalidServiceException : DnsException
    {
        public InvalidServiceException(string message) : base(message) { }
    }

    public class InvalidServiceIdException : InvalidServiceException
    {
        public InvalidServiceIdException(string message) : base(message) { }
    }

    public class EmptyServiceIdException : InvalidServiceIdException
    {
        public EmptyServiceIdException() : base("Service id cannot be empty.") { }
    }
}
