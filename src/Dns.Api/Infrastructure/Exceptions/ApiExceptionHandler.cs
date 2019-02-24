namespace Dns.Api.Infrastructure.Exceptions
{
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;

    public class ApiExceptionHandler : DefaultExceptionHandler<ApiException>
    {
        protected override BasicApiProblem GetApiProblemFor(ApiException exception)
        {
            return new BasicApiProblem
            {
                HttpStatus = exception.StatusCode,
                Title = Constants.DefaultTitle,
                Detail = exception.Message,
                ProblemInstanceUri = BasicApiProblem.GetProblemNumber(),
                ProblemTypeUri = BasicApiProblem.GetTypeUriFor(exception)
            };
        }
    }
}
