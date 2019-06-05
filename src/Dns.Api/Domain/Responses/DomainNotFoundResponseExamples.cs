namespace Dns.Api.Domain.Responses
{
    using Be.Vlaanderen.Basisregisters.BasicApiProblem;
    using Microsoft.AspNetCore.Http;
    using Swashbuckle.AspNetCore.Filters;

    public class DomainNotFoundResponseExamples : IExamplesProvider
    {
        public static string Message = "Domain does not exist.";

        public object GetExamples()
            => new ProblemDetails
            {
                HttpStatus = StatusCodes.Status404NotFound,
                Title = ProblemDetails.DefaultTitle,
                Detail = Message,
                ProblemInstanceUri = ProblemDetails.GetProblemNumber()
            };
    }
}
