using System.Collections.Generic;
using NetCoreApiScaffolding.Application.Common.Request;

namespace NetCoreApiScaffolding.Application.Common
{
    public class PaginatedRequest : IPaginatedRequest, ISortRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public List<Sort> Sort { get; set; } = new List<Sort>();
    }
}
