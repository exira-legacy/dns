namespace Dns.Api.Domain.Exceptions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using FluentValidation;
    using FluentValidation.Results;
    using Infrastructure.Exceptions;
    using Swashbuckle.AspNetCore.Filters;

    public class ValidationExceptionHandling : DefaultExceptionHandler<ValidationException>
    {
        protected override BasicApiProblem GetApiProblemFor(ValidationException exception)
            => new BasicApiValidationProblem(exception);
    }

    public class BasicApiValidationProblem : BasicApiProblem
    {
        public string[] ValidationErrors { get; set; }

        // Here to make DataContractSerializer happy
        public BasicApiValidationProblem() {}

        public BasicApiValidationProblem(ValidationException exception)
        {
            HttpStatus = (int)HttpStatusCode.BadRequest;
            Title = Constants.DefaultTitle;
            Detail = "Validation failed!";
            ProblemInstanceUri = GetProblemNumber();
            ProblemTypeUri = GetTypeUriFor(exception);
            ValidationErrors = exception.Errors.Select(x => x.ErrorMessage).ToArray();
        }
    }

    public class ValidationErrorResponseExamples : IExamplesProvider
    {
        public object GetExamples() =>
            new BasicApiValidationProblem(new ValidationException(string.Empty, new List<ValidationFailure>
            {
                new ValidationFailure(string.Empty, "Top Level Domain must be one of 'be', 'biz', ...")
            }));
    }
}
