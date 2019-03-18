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

    public class InvalidServiceLabelException : InvalidServiceException
    {
        public InvalidServiceLabelException(string message) : base(message) { }
    }

    public class EmptyServiceIdException : InvalidServiceIdException
    {
        public EmptyServiceIdException() : base("Service Id cannot be empty.") { }
    }

    public class EmptyServiceLabelException : InvalidServiceLabelException
    {
        public EmptyServiceLabelException() : base("Service Label cannot be empty.") { }
    }
}
