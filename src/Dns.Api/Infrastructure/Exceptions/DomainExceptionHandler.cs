namespace Dns.Api.Infrastructure.Exceptions
{
    using System.Net;
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;

    public class DomainExceptionHandler : DefaultExceptionHandler<DomainException>
    {
        protected override BasicApiProblem GetApiProblemFor(DomainException exception)
        {
            return new BasicApiProblem
            {
                HttpStatus = (int)HttpStatusCode.BadRequest,
                Title = Constants.DefaultTitle,
                Detail = exception.Message,
                ProblemInstanceUri = BasicApiProblem.GetProblemNumber(),
                ProblemTypeUri = BasicApiProblem.GetTypeUriFor(exception)
            };
        }
    }
}
