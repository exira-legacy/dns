namespace Dns
{
    using System;
    using Be.Vlaanderen.Basisregisters.AggregateSource;

    public abstract class DnsException : DomainException
    {
        protected DnsException(string message) : base(message) { }

        protected DnsException(string message, Exception inner) : base(message, inner) { }
    }
}
