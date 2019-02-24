namespace Dns.Api.Infrastructure.Exceptions
{
    using System.Net;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;

    public class InvalidTopLevelDomainExceptionHandling : DefaultExceptionHandler<InvalidTopLevelDomainException>
    {
        protected override BasicApiProblem GetApiProblemFor(InvalidTopLevelDomainException exception)
        {
            // TODO: Is bad request the best choice?
            return new BasicApiProblem
            {
                HttpStatus = (int)HttpStatusCode.BadRequest,
                Title = Constants.DefaultTitle,
                Detail = $"'{exception.Value}' is not valid for {exception.Type}.",
                ProblemInstanceUri = BasicApiProblem.GetProblemNumber(),
                ProblemTypeUri = BasicApiProblem.GetTypeUriFor(exception)
            };
        }
    }
}
