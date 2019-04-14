namespace Dns.Api.Infrastructure
{
    using System.Collections.Generic;
    using System.Security.Claims;

    public class CommandMetaData
    {
        public static class Keys
        {
            public const string Ip = "Ip";
            public const string CorrelationId = "CorrelationId";
        }

        private readonly ClaimsPrincipal _claimsPrincipal;

        public string Ip { get; private set; }
        public string CorrelationId { get; private set; }

        private CommandMetaData() { }

        public CommandMetaData(
            ClaimsPrincipal claimsPrincipal,
            string ipClaimName,
            string correlationClaimName)
        {
            _claimsPrincipal = claimsPrincipal;

            Ip = claimsPrincipal.FindFirst(ipClaimName)?.Value;
            CorrelationId = claimsPrincipal.FindFirst(correlationClaimName)?.Value;
        }

        public static CommandMetaData FromDictionary(IDictionary<string, object> source) =>
            new CommandMetaData
            {
                Ip = StringOrEmpty(source, Keys.Ip),
                CorrelationId = StringOrEmpty(source, Keys.CorrelationId)
            };


        private static string StringOrEmpty(IDictionary<string, object> source, string key) =>
            source.ContainsKey(key)
                ? (string)source[key]
                : string.Empty;

        public IDictionary<string, object> ToDictionary()
        {
            if (_claimsPrincipal == null)
                return new Dictionary<string, object>();

            return new Dictionary<string, object>
            {
                { Keys.Ip, Ip },
                { Keys.CorrelationId, CorrelationId }
            };
        }
    }
}
