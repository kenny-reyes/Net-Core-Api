using System.Collections.Generic;

namespace DotNetCqrsApi.Application.Shared.Request
{
    public interface ISortRequest
    {
        List<Sort> Sort { get; set; }
    }
}
