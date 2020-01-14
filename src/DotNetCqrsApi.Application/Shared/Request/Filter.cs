using System.Collections.Generic;
using System.Linq;

namespace DotNetCqrsApi.Application.Shared.Request
{
    public class Filter
    {
        public string Field { get; set; }
        public object Value { get; set; }
        public string Operator { get; set; }
        public List<Filter> Filters { get; set; } = new List<Filter>();
        public string Logic { get; set; }

        public bool HasFilters()
        {
            return Filters.Any();
        }

        public IEnumerable<Filter> GetFiltersWithoutChildrenFilters()
        {
            var filters = new List<Filter>();
            foreach (var filter in Filters)
            {
                if (!filter.HasFilters())
                {
                    filters.Add(filter);
                }
                else
                {
                    filters.AddRange(filter.GetFiltersWithoutChildrenFilters());
                }
            }
            return filters;
        }
    }
}
