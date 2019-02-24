namespace Dns.Api.Domain
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Be.Vlaanderen.Basisregisters.Api;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using Be.Vlaanderen.Basisregisters.CommandHandling;
    using Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Converters;
    using Requests;
    using Swashbuckle.AspNetCore.Filters;

    [ApiVersion("1.0")]
    [AdvertiseApiVersions("1.0")]
    [ApiRoute("domains")]
    [ApiExplorerSettings(GroupName = "Domain")]
    public class DomainController : DnsController
    {
        /// <summary>
        /// Register a domain.
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="commandId">Optional unique identifier for the request.</param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="202">If the request has been accepted.</response>
        /// <response code="400">If the request contains invalid data.</response>
        /// <response code="500">If an internal error has occurred.</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(BasicApiProblem), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BasicApiProblem), StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreateDomainRequest), typeof(CreateDomainRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status202Accepted, typeof(CommandResponseExamples), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestResponseExamples), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorResponseExamples), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post(
            [FromServices] ICommandHandlerResolver bus,
            [FromCommandId] Guid commandId,
            [FromBody] CreateDomainRequest request,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = CreateDomainRequestMapping.Map(request);

            // TODO: Sending an empty body should give a proper bad request
            // TODO: Sending null for top level domain should give a decent error, not 500
            // TODO: Registering an existing domain should give a decent error, not 500

            return Accepted(
                $"/v1/domains/{command.DomainName}",
                await bus.Dispatch(
                    commandId,
                    command,
                    GetMetadata(),
                    cancellationToken));
        }
    }

    public class CommandResponseExamples : IExamplesProvider
    {
        public object GetExamples() => new { };
    }
}
