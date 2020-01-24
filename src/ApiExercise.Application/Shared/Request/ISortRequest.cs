using System.Collections.Generic;

namespace ApiExercise.Application.Shared.Request
{
    public interface ISortRequest
    {
        List<Sort> Sort { get; set; }
    }
}
