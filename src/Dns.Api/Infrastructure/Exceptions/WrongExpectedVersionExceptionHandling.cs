namespace Dns.Api.Infrastructure.Exceptions
{
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using SqlStreamStore.Streams;
    using Be.Vlaanderen.Basisregisters.BasicApiProblem;
    using Microsoft.AspNetCore.Http;

    public class WrongExpectedVersionExceptionHandling : DefaultExceptionHandler<WrongExpectedVersionException>
    {
        // TODO: When WrongExpectedVersionException has properties for stream and expected version,
        // we can turn this into a more detailed message
        protected override ProblemDetails GetApiProblemFor(WrongExpectedVersionException exception)
            => new ProblemDetails
            {
                HttpStatus = StatusCodes.Status400BadRequest,
                Title = "This action is not valid!",
                Detail = exception.Message,
                ProblemInstanceUri = ProblemDetails.GetProblemNumber(),
                ProblemTypeUri = ProblemDetails.GetTypeUriFor(exception)
            };
    }
}
