using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Risly.Cqrs;
using StudyApi.Commands;
using StudyApi.Controllers.Resources;

namespace StudyApi.Controllers
{
    public class StudiesController : Controller
    {
        private readonly ICommandBus _commandBus;
        public StudiesController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        [HttpGet, Route("api/studies")]
        public IEnumerable<StudyResource> GetStudies()
        {
            return new StudyResource[] {
                StudyResource.Generate()
            };
        }

        [HttpPost, Route("api/studies")]
        public IActionResult CreateStudy([FromBody]StudyResource studyResource)
        {
            Guid studyId = Guid.NewGuid();
            var command = new CreateStudyCommand(studyId, studyResource.Facility.Id, studyResource.Facility.Name,
                studyResource.AccessionNumber, studyResource.Procedure.Id, studyResource.Procedure.Name,
                studyResource.ProcedureDate, studyResource.Reason, studyResource.ImageSet.Id, studyResource.ImageSet.DicomInstanceId,
                studyResource.ImageSet.TotalImages, studyResource.ImageSet.ImagesReceived, studyResource.Patient.FirstName,
                studyResource.Patient.LastName, studyResource.Patient.Gender, studyResource.Patient.DateOfBirth, studyResource.Patient.Mrn,
                studyResource.ReferringPhysician.FirstName, studyResource.ReferringPhysician.LastName);

            _commandBus.Send(command);
            
            return Ok(studyId.ToString() + " created");
        }

        [HttpPost, Route("api/studies/review")]
        public IActionResult ReviewStudy([FromBody]Guid studyId)
        {
            var command = new ReviewStudyCommand(studyId);
            
            _commandBus.Send(command);

            return Ok(studyId.ToString() + " reviewed");
        }
    }
}