namespace Dns
{
    using System;

    public class InvalidHostnameException : DnsException
    {
        public InvalidHostnameException() { }

        public InvalidHostnameException(string message) : base(message) { }

        public InvalidHostnameException(string message, Exception inner) : base(message, inner) { }
    }
}
