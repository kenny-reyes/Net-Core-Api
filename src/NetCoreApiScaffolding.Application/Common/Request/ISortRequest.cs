using System.Collections.Generic;

namespace NetCoreApiScaffolding.Application.Common.Request
{
    public interface ISortRequest
    {
        List<Sort> Sort { get; set; }
    }
}
