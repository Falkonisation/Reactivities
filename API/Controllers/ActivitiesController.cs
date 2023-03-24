using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    
    public class ActivitiesController :BaseApiController
    {
        private readonly DataContext _context;
        public ActivitiesController(DataContext context)
        {
            _context=context;
        }
        [HttpGet]
        public async Task<ActionResult<List<activity>>> GetActivities()
        {

            return await _context.Activities.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<activity>> getActivity(Guid id)
        {
            return await _context.Activities.FindAsync(id);
        }
    }
}