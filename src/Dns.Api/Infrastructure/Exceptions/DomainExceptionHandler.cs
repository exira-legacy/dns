namespace Dns.Api.Infrastructure.Exceptions
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using Be.Vlaanderen.Basisregisters.BasicApiProblem;
    using Microsoft.AspNetCore.Http;

    public class DomainExceptionHandler : DefaultExceptionHandler<DomainException>
    {
        protected override ProblemDetails GetApiProblemFor(DomainException exception)
            => new ProblemDetails
            {
                HttpStatus = StatusCodes.Status400BadRequest,
                Title = Constants.DefaultTitle,
                Detail = exception.Message,
                ProblemInstanceUri = ProblemDetails.GetProblemNumber(),
                ProblemTypeUri = ProblemDetails.GetTypeUriFor(exception)
            };
    }
}
