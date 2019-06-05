namespace Dns.Api.Infrastructure.Exceptions
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using Be.Vlaanderen.Basisregisters.BasicApiProblem;
    using Microsoft.AspNetCore.Http;

    public class AggregateNotFoundExceptionHandling : DefaultExceptionHandler<AggregateNotFoundException>
    {
        protected override ProblemDetails GetApiProblemFor(AggregateNotFoundException exception)
            => new ProblemDetails
            {
                HttpStatus = StatusCodes.Status400BadRequest,
                Title = "This action is not valid!",
                Detail = $"The resource with id '{exception.Identifier}' does not exist.",
                ProblemInstanceUri = ProblemDetails.GetProblemNumber(),
                ProblemTypeUri = ProblemDetails.GetTypeUriFor(exception)
            };
    }
}
