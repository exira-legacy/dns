namespace Dns.Api.Domain.Responses
{
    using Be.Vlaanderen.Basisregisters.BasicApiProblem;
    using Microsoft.AspNetCore.Http;
    using Swashbuckle.AspNetCore.Filters;

    public class ServiceNotFoundResponseExamples : IExamplesProvider
    {
        public static string Message = "Service does not exist.";

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
