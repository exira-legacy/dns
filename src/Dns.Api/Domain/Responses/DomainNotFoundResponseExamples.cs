namespace Dns.Api.Domain.Responses
{
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Swashbuckle.AspNetCore.Filters;

    public class DomainNotFoundResponseExamples : IExamplesProvider
    {
        public static string Message = "Domain does not exist.";

        public object GetExamples()
            => new BasicApiProblem
            {
                HttpStatus = StatusCodes.Status404NotFound,
                Title = BasicApiProblem.DefaultTitle,
                Detail = Message,
                ProblemInstanceUri = BasicApiProblem.GetProblemNumber()
            };
    }
}
