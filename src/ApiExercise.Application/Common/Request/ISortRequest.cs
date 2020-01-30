using System.Collections.Generic;

namespace ApiExercise.Application.Common.Request
{
    public interface ISortRequest
    {
        List<Sort> Sort { get; set; }
    }
}
