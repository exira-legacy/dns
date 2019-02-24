namespace Dns.Api.Infrastructure.Exceptions
{
    using System.Net;
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;

    public class AggregateNotFoundExceptionHandling : DefaultExceptionHandler<AggregateNotFoundException>
    {
        protected override BasicApiProblem GetApiProblemFor(AggregateNotFoundException exception)
        {
            return new BasicApiProblem
            {
                HttpStatus = (int)HttpStatusCode.BadRequest,
                Title = "This action is not valid!",
                Detail = $"The resource with id '{exception.Identifier}' does not exist.",
                ProblemInstanceUri = BasicApiProblem.GetProblemNumber(),
                ProblemTypeUri = BasicApiProblem.GetTypeUriFor(exception)
            };
        }
    }
}
