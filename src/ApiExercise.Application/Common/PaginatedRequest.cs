using System.Collections.Generic;
using ApiExercise.Application.Common.Request;

namespace ApiExercise.Application.Common
{
    public class PaginatedRequest : IPaginatedRequest, ISortRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public List<Sort> Sort { get; set; } = new List<Sort>();
    }
}
