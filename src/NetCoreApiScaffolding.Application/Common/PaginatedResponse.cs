using System.Collections.Generic;

namespace NetCoreApiScaffolding.Application.Common
{
    public class PaginatedResponse<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Data { get; }
        public long Total { get; }

        public PaginatedResponse(IEnumerable<TEntity> data, long total)
        {
            Data = data;
            Total = total;
        }
    }
}