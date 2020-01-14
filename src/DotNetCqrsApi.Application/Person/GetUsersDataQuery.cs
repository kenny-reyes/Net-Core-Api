using System.Threading;
using System.Threading.Tasks;
using DotNetCqrsApi.Application.Person.Queries;
using DotNetCqrsApi.Application.Person.Responses;
using DotNetCqrsApi.Application.Shared;
using MediatR;

namespace DotNetCqrsApi.Application.Person
{
    public class GetPeopleDataQueryRequest : PaginatedRequest, IRequest<PaginatedResponse<PersonListItemModel>>
    { }
    
    public class GetUsersDataQueryHandler : IRequestHandler<GetPeopleDataQueryRequest, PaginatedResponse<PersonListItemModel>>
    {                                                                         
        private readonly IGetPeople _getPeopleQueries;

        public GetUsersDataQueryHandler(IGetPeople getPeopleQueries)
        {
            this._getPeopleQueries = getPeopleQueries;
        }
        
        public async Task<PaginatedResponse<PersonListItemModel>> Handle(GetPeopleDataQueryRequest request, CancellationToken cancellationToken)
        {
            return await _getPeopleQueries.Query(request, cancellationToken);
        }
    }
}
