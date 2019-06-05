namespace Dns.Api.Infrastructure.Exceptions
{
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using Be.Vlaanderen.Basisregisters.BasicApiProblem;

    public class ApiExceptionHandler : DefaultExceptionHandler<ApiException>
    {
        protected override ProblemDetails GetApiProblemFor(ApiException exception)
            => new ProblemDetails
            {
                HttpStatus = exception.StatusCode,
                Title = Constants.DefaultTitle,
                Detail = exception.Message,
                ProblemInstanceUri = ProblemDetails.GetProblemNumber(),
                ProblemTypeUri = ProblemDetails.GetTypeUriFor(exception)
            };
    }
}
