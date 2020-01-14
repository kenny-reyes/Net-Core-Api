﻿using System.Threading;
using System.Threading.Tasks;
using DotNetCqrsApi.Application.Person.Queries;
using DotNetCqrsApi.Application.Person.Responses;
using DotNetCqrsApi.Application.Shared;
using MediatR;

namespace DotNetCqrsApi.Application.Person
{
    public class GetUsersDataQueryRequest : PaginatedRequest, IRequest<PaginatedResponse<PersonListItemModel>>
    { }
    
    public class GetUsersDataQueryHandler : IRequestHandler<GetUsersDataQueryRequest, PaginatedResponse<PersonListItemModel>>
    {                                                                         
        private readonly IGetUsers getUsersQueries;

        public GetUsersDataQueryHandler(IGetUsers getUsersQueries)
        {
            this.getUsersQueries = getUsersQueries;
        }
        
        public async Task<PaginatedResponse<PersonListItemModel>> Handle(GetUsersDataQueryRequest request, CancellationToken cancellationToken)
        {
            return await getUsersQueries.Query(request, cancellationToken);
        }
    }
}
