namespace Dns.Api.Infrastructure.Exceptions
{
    using System.Net;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using SqlStreamStore.Streams;

    public class WrongExpectedVersionExceptionHandling : DefaultExceptionHandler<WrongExpectedVersionException>
    {
        protected override BasicApiProblem GetApiProblemFor(WrongExpectedVersionException exception)
        {
            // TODO: When WrongExpectedVersionException has properties for stream and expected version,
            // we can turn this into a more detailed message
            return new BasicApiProblem
            {
                HttpStatus = (int)HttpStatusCode.BadRequest,
                Title = "This action is not valid!",
                Detail = exception.Message,
                ProblemInstanceUri = BasicApiProblem.GetProblemNumber(),
                ProblemTypeUri = BasicApiProblem.GetTypeUriFor(exception)
            };
        }
    }
}
