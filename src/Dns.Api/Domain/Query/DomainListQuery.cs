namespace Dns.Api.Domain.Query
{
    using System.Collections.Generic;
    using System.Linq;
    using Be.Vlaanderen.Basisregisters.Api.Search;
    using Be.Vlaanderen.Basisregisters.Api.Search.Filtering;
    using Be.Vlaanderen.Basisregisters.Api.Search.Sorting;
    using Microsoft.EntityFrameworkCore;
    using Projections.Api;
    using Projections.Api.DomainList;

    public class DomainListQuery : Query<DomainList, DomainListFilter>
    {
        private readonly ApiProjectionsContext _context;

        protected override ISorting Sorting => new DomainSorting();

        public DomainListQuery(ApiProjectionsContext context) => _context = context;

        protected override IQueryable<DomainList> Filter(FilteringHeader<DomainListFilter> filtering)
        {
            var domains = _context
                .DomainList
                .AsNoTracking();

            if (!filtering.ShouldFilter)
                return domains;

            if (!string.IsNullOrEmpty(filtering.Filter.SecondLevelDomain))
                domains = domains.Where(m => m.SecondLevelDomain == filtering.Filter.SecondLevelDomain);

            if (!string.IsNullOrEmpty(filtering.Filter.TopLevelDomain))
                domains = domains.Where(m => m.TopLevelDomain == filtering.Filter.TopLevelDomain);

            return domains;
        }

        internal class DomainSorting : ISorting
        {
            public IEnumerable<string> SortableFields { get; } = new[]
            {
                nameof(DomainList.Name),
                nameof(DomainList.SecondLevelDomain),
                nameof(DomainList.TopLevelDomain),
            };

            public SortingHeader DefaultSortingHeader { get; } = new SortingHeader(nameof(DomainList.Name), SortOrder.Ascending);
        }
    }

    public class DomainListFilter
    {
        public string SecondLevelDomain { get; set; }
        public string TopLevelDomain { get; set; }
    }
}
