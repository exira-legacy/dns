namespace Dns
{
    using System.Diagnostics.CodeAnalysis;

    public class InvalidTopLevelDomainException : EnumerationException
    {
        public InvalidTopLevelDomainException(string message, string paramName, object value, string type) :
            base(message, paramName, value, type) { }
    }

    public class TopLevelDomain : Enumeration<TopLevelDomain, string, InvalidTopLevelDomainException>
    {
        public string FullName { get; }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static readonly TopLevelDomain
            be = new TopLevelDomain("be", ".be", "Belgium"),
            eu = new TopLevelDomain("eu", ".eu", "Europe"),
            nl = new TopLevelDomain("nl", ".nl", "The Netherlands"),
            com = new TopLevelDomain("com", ".com", "Commercial");

        private TopLevelDomain(
            string extension,
            string displayName,
            string fullName) : base(extension, displayName)
            => FullName = fullName;
    }
}
