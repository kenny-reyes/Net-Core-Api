using System.Collections.Generic;
using DotNetCqrsApi.Application.Shared.Request;

namespace DotNetCqrsApi.Application.Shared
{
    public class PaginatedRequest : IFilterRequest, IPaginatedRequest, ISortRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public List<Sort> Sort { get; set; } = new List<Sort>();
        public Filter Filter { get; set; } = new Filter();
    }
}
