namespace Dns.Api.Domain.Exceptions
{
    using System.Net;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using Infrastructure.Exceptions;

    public class InvalidServiceTypeExceptionHandling : DefaultExceptionHandler<InvalidServiceTypeException>
    {
        protected override BasicApiProblem GetApiProblemFor(InvalidServiceTypeException exception)
        {
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
