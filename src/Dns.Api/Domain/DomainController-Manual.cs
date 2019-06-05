namespace Dns.Api.Domain
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Be.Vlaanderen.Basisregisters.Api;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using Be.Vlaanderen.Basisregisters.CommandHandling;
    using Infrastructure.LastObservedPosition;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Converters;
    using Projections.Api;
    using Requests;
    using Swashbuckle.AspNetCore.Filters;
    using ProblemDetails = Be.Vlaanderen.Basisregisters.BasicApiProblem.ProblemDetails;

    public partial class DomainController
    {
        /// <summary>
        /// Add a manual service to a domain.
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="context"></param>
        /// <param name="commandId">Optional unique identifier for the request.</param>
        /// <param name="secondLevelDomain">Second level domain of the domain to add a manual service to.</param>
        /// <param name="topLevelDomain">Top level domain of the domain to add a manual service to.</param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="202">If the request has been accepted.</response>
        /// <response code="400">If the request contains invalid data.</response>
        /// <response code="500">If an internal error has occurred.</response>
        /// <returns></returns>
        [HttpPost("{secondLevelDomain}.{topLevelDomain}/services/manual")]
        [ProducesResponseType(typeof(LastObservedPositionResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(AddManualServiceRequest), typeof(AddManualServiceRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status202Accepted, typeof(LastObservedPositionResponseExamples), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationErrorResponseExamples), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorResponseExamples), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> AddManualService(
            [FromServices] ICommandHandlerResolver bus,
            [FromServices] ApiProjectionsContext context,
            [FromCommandId] Guid commandId,
            [FromRoute] string secondLevelDomain,
            [FromRoute] string topLevelDomain,
            [FromBody] AddManualServiceRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request != null)
            {
                request.SecondLevelDomain = secondLevelDomain;
                request.TopLevelDomain = topLevelDomain;
            }

            // TODO: We can check in the eventstore if those aggregates even exist
            await new AddManualServiceRequestValidator()
                .ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

            var command = AddManualServiceRequestMapping.Map(
                new DomainName(
                    new SecondLevelDomain(secondLevelDomain),
                    TopLevelDomain.FromValue(topLevelDomain)),
                request);

            return Accepted(
                $"/v1/domains/{command.DomainName}/services/{command.ServiceId}",
                new LastObservedPositionResponse(
                    await bus.Dispatch(
                        commandId,
                        command,
                        GetMetadata(),
                        cancellationToken)));
        }
    }
}
