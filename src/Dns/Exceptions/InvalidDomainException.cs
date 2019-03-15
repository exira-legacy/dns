namespace Dns.Exceptions
{
    public class InvalidDomainException : DnsException
    {
        public InvalidDomainException(string message) : base(message) { }
    }

    public class InvalidSecondLevelDomainException : InvalidDomainException
    {
        public InvalidSecondLevelDomainException(string message) : base(message) { }
    }

    public class EmptySecondLevelDomainException : InvalidSecondLevelDomainException
    {
        public EmptySecondLevelDomainException() : base("Name of a second level domain cannot be empty.") { }
    }

    public class SecondLevelDomainTooLongException : InvalidSecondLevelDomainException
    {
        public SecondLevelDomainTooLongException() : base($"Name of a second level domain cannot be longer than {SecondLevelDomain.MaxLength} characters.") { }
    }

    public class InvalidHostNameException : InvalidSecondLevelDomainException
    {
        public InvalidHostNameException() : base("Name of a second level domain must be a valid hostname.") { }
    }
}
