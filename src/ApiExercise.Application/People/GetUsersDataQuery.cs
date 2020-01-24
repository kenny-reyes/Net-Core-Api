using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.People.Queries;
using ApiExercise.Application.People.Responses;
using ApiExercise.Application.Shared;
using MediatR;

namespace ApiExercise.Application.People
{
    public class GetPeopleDataQueryRequest : PaginatedRequest, IRequest<PaginatedResponse<PersonListItemModel>>
    { }
    
    public class GetPeopleDataQueryHandler : IRequestHandler<GetPeopleDataQueryRequest, PaginatedResponse<PersonListItemModel>>
    {                                                                         
        private readonly IGetPeople _getPeopleQueries;

        public GetPeopleDataQueryHandler(IGetPeople getPeopleQueries)
        {
            this._getPeopleQueries = getPeopleQueries;
        }
        
        public async Task<PaginatedResponse<PersonListItemModel>> Handle(GetPeopleDataQueryRequest request, CancellationToken cancellationToken)
        {
            return await _getPeopleQueries.Query(request, cancellationToken);
        }
    }
}
