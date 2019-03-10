namespace Dns.Importer
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using MySql.Data.MySqlClient;

    public class DomainFetcher
    {
        private const string DomainSql = "SELECT DomainId, ExternalId FROM Dns.Domain;";
        private const string RecordSql = "SELECT Type, HostName, TimeToLive, Value FROM Dns.ResourceRecord WHERE DomainId = @DomainId;";

        private readonly ILogger<DomainFetcher> _logger;
        private readonly IConfiguration _configuration;

        public class Domain
        {
            public int DomainId { get; set; }
            public string ExternalId { get; set; }
        }

        public class Record
        {
            public string Type { get; set; }
            public string HostName { get; set; }
            public string TimeToLive { get; set; }
            public string Value { get; set; }
        }

        public DomainFetcher(
            ILogger<DomainFetcher> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Dictionary<Domain, List<Record>>> FetchAsync()
        {
            using (IDbConnection conn = new MySqlConnection(_configuration.GetConnectionString("Legacy")))
            { 
                var domains = (await conn.QueryAsync<Domain>(DomainSql)).ToList();
                var result = new Dictionary<Domain, List<Record>>();

                _logger.LogInformation("Fetched {NumberOfDomains} domains.", domains.Count());
                foreach (var domain in domains)
                {
                    result.Add(domain, new List<Record>());

                    var records = (await conn.QueryAsync<Record>(RecordSql, new { DomainId = domain.DomainId })).ToList();

                    _logger.LogInformation("Fetched {NumberOfRecords} records for {Domain}.", records.Count(), domain.ExternalId);
                    result[domain].AddRange(records);
                }

                return result;
            }
        }
    }
}
