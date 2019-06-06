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
            biz = new TopLevelDomain("biz", ".biz", "Business"),
            brussels = new TopLevelDomain("brussels", ".brussels", "Brussels"),
            cn = new TopLevelDomain("cn", ".cn", "China"),
            com = new TopLevelDomain("com", ".com", "Commercial"),
            consulting = new TopLevelDomain("consulting", ".consulting", "Consulting"),
            design = new TopLevelDomain("design", ".design", "Design"),
            domains = new TopLevelDomain("domains", ".domains", "Domains"),
            es = new TopLevelDomain("es", ".es", "Spain"),
            eu = new TopLevelDomain("eu", ".eu", "Europe"),
            fr = new TopLevelDomain("fr", ".fr", "France"),
            gent = new TopLevelDomain("gent", ".gent", "Ghent"),
            ie = new TopLevelDomain("ie", ".ie", "Republic of Ireland"),
            @in = new TopLevelDomain("in", ".in", "India"),
            info = new TopLevelDomain("info", ".info", "Information"),
            io = new TopLevelDomain("io", ".io", "British Indian Ocean Territory"),
            lu = new TopLevelDomain("lu", ".lu", "Luxembourg"),
            me = new TopLevelDomain("me", ".me", "Montenegro"),
            mobi = new TopLevelDomain("mobi", ".mobi", "Mobile"),
            net = new TopLevelDomain("net", ".net", "Network"),
            nl = new TopLevelDomain("nl", ".nl", "The Netherlands"),
            nu = new TopLevelDomain("nu", ".nu", "Niue"),
            one = new TopLevelDomain("one", ".one", "One"),
            org = new TopLevelDomain("org", ".org", "Organisation"),
            se = new TopLevelDomain("se", ".se", "Sweden"),
            vlaanderen = new TopLevelDomain("vlaanderen", ".vlaanderen", "Flanders"),
            pro = new TopLevelDomain("pro", ".pro", "Professional"),
            immo = new TopLevelDomain("immo", ".immo", "Real Estate");

        private TopLevelDomain(
            string extension,
            string displayName,
            string fullName) : base(extension?.ToLowerInvariant(), displayName)
            => FullName = fullName;
    }
}
