using System;
using Microsoft.AspNetCore.Mvc;
using FacilityApi.Commands;
using FacilityApi.Controllers.Resources;
using Risly.Cqrs;

namespace FacilityApi.Controllers
{
    [Route("api/[controller]")]
    public class FacilitiesController : Controller
    {
        private readonly ICommandBus _commandBus;

        public FacilitiesController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        // GET api/facilities
        [HttpGet]
        public System.Collections.Generic.IEnumerable<string> Get()
        {
            return new string[] { "facility1", "facility2" };
        }
        
        // POST api/facilities
        [HttpPost]
        public IActionResult Post([FromBody]CreateFacilityResource facilityResource)
        {
            _commandBus.Send(new CreateFacilityCommand(Guid.NewGuid(), facilityResource.FacilityName));
            
            return Ok();
        }
    }
}