namespace Dns.Projections.Api.DomainDetail
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.Connector;

    public static class DomainDetailExtensions
    {
        public static async Task<DomainDetail> FindAndUpdateDomainDetail(
            this ApiProjectionsContext context,
            string domainName,
            Action<DomainDetail> updateFunc,
            CancellationToken ct)
        {
            var domain = await context
                .DomainDetails
                .FindAsync(domainName, cancellationToken: ct);

            if (domain == null)
                throw DatabaseItemNotFound(domainName);

            updateFunc(domain);

            return domain;
        }

        private static ProjectionItemNotFoundException<DomainDetailProjections> DatabaseItemNotFound(string domainName)
            => new ProjectionItemNotFoundException<DomainDetailProjections>(domainName);
    }
}
