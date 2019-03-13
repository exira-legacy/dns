namespace Dns.Api.Domain.Responses
{
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Swashbuckle.AspNetCore.Filters;

    public class DomainNotFoundResponseExamples : IExamplesProvider
    {
        public object GetExamples()
            => new BasicApiProblem
            {
                HttpStatus = StatusCodes.Status404NotFound,
                Title = BasicApiProblem.DefaultTitle,
                Detail = "Domain does not exist.",
                ProblemInstanceUri = BasicApiProblem.GetProblemNumber()
            };
    }
}