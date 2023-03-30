using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Activities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    
    public class ActivitiesController :BaseApiController
    {


        [HttpGet]
        public async Task<ActionResult<List<activity>>> GetActivities()
        {

            return await Mediator.Send(new List.Query(),ct);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<activity>> getActivity(Guid Id)
        {
            return await Mediator.Send(new Details.Query{id=Id} );
        }
        [HttpPost]
        public async Task<IActionResult> CreateActivity(activity act)
        {
            return Ok(await Mediator.Send(new Create.command {Activity=act})) ;
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> EditActivity(Guid Id,activity act)
        {
            act.Id=Id;


            return Ok(await Mediator.Send(new Edit.Command{Activity=act}));
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> deleteActivity(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command{Id=id}));
        }
    }
}