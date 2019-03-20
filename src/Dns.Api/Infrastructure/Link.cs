namespace Dns.Api.Infrastructure
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Link", Namespace = "")]
    public class Link
    {
        public class Relations
        {
            public const string Home = "home";
            public const string Domains = "domains";
            public const string Domain = "domain";
            public const string Services = "services";
            public const string Service = "service";
        }

        [DataMember(Name = "Href", Order = 1)]
        public string Href { get; set; }

        [DataMember(Name = "Rel", Order = 2)]
        public string Rel { get; set; }

        [DataMember(Name = "Type", Order = 3)]
        public string Type { get; set; }

        public Link(string href, string rel, string type)
        {
            Href = href;
            Rel = rel;
            Type = type;
        }
    }
}
