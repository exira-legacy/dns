namespace Dns.Importer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Modules;

    public class DnsImporter
    {
        private readonly ILogger<DnsImporter> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly DomainFetcher _domainFetcher;

        public DnsImporter(
            ILogger<DnsImporter> logger,
            IHttpClientFactory httpClientFactory,
            DomainFetcher domainFetcher)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _domainFetcher = domainFetcher;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var domains = await _domainFetcher.FetchAsync();

            foreach (var domain in domains)
                SendToApi(domain);
        }

        private void SendToApi(KeyValuePair<DomainFetcher.Domain, List<DomainFetcher.Record>> domain)
        {
            using (var client = _httpClientFactory.CreateClient(HttpModule.HttpClientName))
            {
                SendToApi(client, string.Empty, () => CreateDomainHttpContent(domain.Key));
                SendToApi(client, $"/{domain.Key.ExternalId}/services/manual", () => CreateRecordHttpContent(domain.Value));
            }
        }

        private void SendToApi(HttpClient client, string endpoint, Func<HttpContent> getHttpContent)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"v1/domains{endpoint}"))
            using (var httpContent = getHttpContent())
            {
                request.Content = httpContent;

                using (var response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).GetAwaiter().GetResult())
                    response.EnsureSuccessStatusCode();
            }
        }

        private HttpContent CreateDomainHttpContent(DomainFetcher.Domain domain)
        {
            var json = $@"{{ secondLevelDomain: ""{domain.SecondLevelDomain}"", topLevelDomain: ""{domain.TopLevelDomain}"" }}";
            _logger.LogDebug("Sending Dns payload: {payload}", json);
            return new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        }

        private HttpContent CreateRecordHttpContent(IEnumerable<DomainFetcher.Record> records)
        {
            // 21600 = 6h, which apparently all of them still use
            var recordsJson = string.Join(",", records.Select(x =>
                $@"{{ type: ""{x.Type}"", timeToLive: 21600, label: ""{x.HostName}"", value: ""{x.Value}"" }}"));

            var json = $@"{{ serviceId: ""{Guid.NewGuid()}"", label: ""Imported Records"", records: [{recordsJson}] }}";
            _logger.LogDebug("Sending Dns payload: {payload}", json);
            return new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        }
    }
}
