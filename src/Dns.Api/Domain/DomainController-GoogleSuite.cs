namespace Dns.Api.Domain
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Be.Vlaanderen.Basisregisters.Api;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using Be.Vlaanderen.Basisregisters.CommandHandling;
    using Exceptions;
    using Infrastructure.Responses;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Converters;
    using Projections.Api;
    using Requests;
    using Swashbuckle.AspNetCore.Filters;

    public partial class DomainController
    {
        /// <summary>
        /// Add a Google Suite service to a domain.
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="context"></param>
        /// <param name="commandId">Optional unique identifier for the request.</param>
        /// <param name="secondLevelDomain">Second level domain of the domain to add a Google Suite service to.</param>
        /// <param name="topLevelDomain">Top level domain of the domain to add a Google Suite service to.</param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="202">If the request has been accepted.</response>
        /// <response code="400">If the request contains invalid data.</response>
        /// <response code="500">If an internal error has occurred.</response>
        /// <returns></returns>
        [HttpPost("{secondLevelDomain}.{topLevelDomain}/services/googlesuite")]
        [ProducesResponseType(typeof(void), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(BasicApiValidationProblem), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BasicApiProblem), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(AddGoogleSuiteServiceRequest), typeof(AddGoogleSuiteServiceRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status202Accepted, typeof(EmptyResponseExamples), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationErrorResponseExamples), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorResponseExamples), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> AddGoogleSuiteService(
            [FromServices] ICommandHandlerResolver bus,
            [FromServices] ApiProjectionsContext context,
            [FromCommandId] Guid commandId,
            [FromRoute] string secondLevelDomain,
            [FromRoute] string topLevelDomain,
            [FromBody] AddGoogleSuiteServiceRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request != null)
            {
                request.SecondLevelDomain = secondLevelDomain;
                request.TopLevelDomain = topLevelDomain;
            }

            await new AddGoogleSuiteServiceRequestValidator()
                .ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

            await FindDomainAsync(context, secondLevelDomain, topLevelDomain, cancellationToken);

            var command = AddGoogleSuiteServiceRequestMapping.Map(
                new DomainName(
                    new SecondLevelDomain(secondLevelDomain),
                    TopLevelDomain.FromValue(topLevelDomain)),
                request);

            return Accepted(
                $"/v1/domains/{command.DomainName}/services/{command.ServiceId}",
                await bus.Dispatch(
                    commandId,
                    command,
                    GetMetadata(),
                    cancellationToken));
        }
    }
}
