namespace Dns.Importer
{
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

        public DnsImporter(
            ILogger<DnsImporter> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // TODO: Implement
        }

        private void SendToApi(object stuff)
        {
            using (var client = _httpClientFactory.CreateClient(HttpModule.HttpClientName))
            using (var request = new HttpRequestMessage(HttpMethod.Post, "v1/domains"))
            using (var httpContent = CreateHttpContent(stuff))
            {
                request.Content = httpContent;

                using (var response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).GetAwaiter().GetResult())
                    response.EnsureSuccessStatusCode();
            }
        }

        private HttpContent CreateHttpContent(object stuff)
        {
            var json = $@"{{ stuff: ""{stuff}"" }}";
            _logger.LogDebug("Sending Dns payload: {payload}", json);
            return new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        }
    }
}
