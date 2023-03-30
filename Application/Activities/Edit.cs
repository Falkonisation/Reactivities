using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command:IRequest
        {
            public activity Activity { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly IMapper _mapper;
            private readonly DataContext _Context;

            public Handler(DataContext Con,IMapper Mapper)
            {
                _mapper=Mapper;
                _Context=Con;

            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity= await _Context.Activities.FindAsync(request.Activity.Id);
                _mapper.Map(request.Activity,activity);
                await _Context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}