using System.Collections.Generic;

namespace DotNetCqrsApi.Application.Shared
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
