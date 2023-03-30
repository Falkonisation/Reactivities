using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query:IRequest<List<activity>>{};
        public class Handler : IRequestHandler<Query, List<activity>>
        {
        public readonly DataContext _Context ;

            public Handler(DataContext context)
            {
             _Context = context;
            }

            public async Task<List<activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _Context.Activities.ToListAsync();
            }
        }
    }
}