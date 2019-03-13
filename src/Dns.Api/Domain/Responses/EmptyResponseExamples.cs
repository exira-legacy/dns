namespace Dns.Api.Domain.Responses
{
    using Swashbuckle.AspNetCore.Filters;

    public class EmptyResponseExamples : IExamplesProvider
    {
        public object GetExamples() => new { };
    }
}
