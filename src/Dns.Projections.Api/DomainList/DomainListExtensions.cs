namespace Dns.Projections.Api.DomainList
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.Connector;

    public static class DomainListExtensions
    {
        public static async Task<DomainList> FindAndUpdateDomainList(
            this ApiProjectionsContext context,
            string domainName,
            Action<DomainList> updateFunc,
            CancellationToken ct)
        {
            var domain = await context
                .DomainList
                .FindAsync(domainName, cancellationToken: ct);

            if (domain == null)
                throw DatabaseItemNotFound(domainName);

            updateFunc(domain);

            return domain;
        }

        private static ProjectionItemNotFoundException<DomainListProjections> DatabaseItemNotFound(string domainName)
            => new ProjectionItemNotFoundException<DomainListProjections>(domainName);
    }
}
