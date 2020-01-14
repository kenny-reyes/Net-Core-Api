﻿using System.Threading;
using System.Threading.Tasks;
using DotNetCqrsApi.Domain.People;
using DotNetCqrsApi.Domain.Repositories.People;
using DotNetCqrsApi.Infrastructure.Context;
using DotNetCqrsApi.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DotNetCqrsApi.Infrastructure.Repositories.People
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(MyContext context)
           : base(context)
        { }

        public async Task<Person> FindByName(string name, CancellationToken cancellationToken)
        {
            return await Context.Set<Person>()
                             .SingleOrDefaultAsync(u => u.Name == name, cancellationToken);
        }

        public async Task<Person> FindById(int id, CancellationToken cancellationToken)
        {
            return await Context.Set<Person>()
                             .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        }


        public async Task<Person> FindByIdWithIncludes(int id, CancellationToken cancellationToken)
        {
            return await Context.Set<Person>()
                            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        }
    }
}
