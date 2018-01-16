using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Risly.Cqrs;
using StudyApi.Commands;
using StudyApi.Controllers.Resources;

namespace StudyApi.Controllers
{
    [Route("api/[controller]")]
    public class StudiesController : Controller
    {
        private readonly ICommandBus _commandBus;
        public StudiesController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        // GET api/studies
        [HttpGet]
        public IEnumerable<StudyResource> Get()
        {
            return new StudyResource[] {
                StudyResource.Generate()
            };
        }

        // POST api/studies
        [HttpPost]
        public IActionResult Post([FromBody]StudyResource studyResource)
        {
            var command = new CreateStudyCommand(Guid.NewGuid(), studyResource.Facility.Id, studyResource.Facility.Name,
                studyResource.AccessionNumber, studyResource.Procedure.Id, studyResource.Procedure.Name,
                studyResource.ProcedureDate, studyResource.Reason, studyResource.ImageSet.Id, studyResource.ImageSet.DicomInstanceId,
                studyResource.ImageSet.TotalImages, studyResource.ImageSet.ImagesReceived, studyResource.Patient.FirstName,
                studyResource.Patient.LastName, studyResource.Patient.Gender, studyResource.Patient.DateOfBirth, studyResource.Patient.Mrn,
                studyResource.ReferringPhysician.FirstName, studyResource.ReferringPhysician.LastName);

            _commandBus.Send(command);
            
            return Ok();
        }
    }
}