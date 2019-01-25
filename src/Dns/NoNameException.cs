namespace Dns
{
    using System;

    public class NoNameException : DnsException
    {
        public NoNameException() { }

        public NoNameException(string message) : base(message) { }

        public NoNameException(string message, Exception inner) : base(message, inner) { }
    }
}
