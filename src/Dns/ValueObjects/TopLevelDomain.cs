namespace Dns
{
    using System.Diagnostics.CodeAnalysis;

    public class TopLevelDomain : Enumeration<TopLevelDomain, string>
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static readonly TopLevelDomain
            be = new TopLevelDomain("be", ".be"),
            eu = new TopLevelDomain("eu", ".eu"),
            nl = new TopLevelDomain("nl", ".nl"),
            com = new TopLevelDomain("com", ".com");

        private TopLevelDomain(string extension, string displayName) : base(extension, displayName) { }
    }
}
