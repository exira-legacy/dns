namespace Dns
{
    using System;

    public class NoExampleIdException : DnsException
    {
        public NoExampleIdException() { }

        public NoExampleIdException(string message) : base(message) { }

        public NoExampleIdException(string message, Exception inner) : base(message, inner) { }
    }
}
