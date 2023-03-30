using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query :IRequest<activity>
        {
            public Guid id { get; set; }
        }
        public class Handler : IRequestHandler<Query, activity>
        {
            private readonly DataContext _Context ;
            public Handler(DataContext context)
            {
             _Context = context;
            }
            public async Task<activity> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _Context.Activities.FindAsync(request.id);
            }
        }
    }
}