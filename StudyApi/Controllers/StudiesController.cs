using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Risly.Cqrs;
using StudyApi.Commands;
using StudyApi.Controllers.Resources;
using StudyApi.Models;
using StudyApi.Queries;

namespace StudyApi.Controllers
{
    public class StudiesController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly IMapper _mapper;

        public StudiesController(ICommandBus commandBus, IQueryBus queryBus, IMapper mapper)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
            _mapper = mapper;
        }

        [HttpGet, Route("api/studies")]
        public IEnumerable<StudyResource> GetStudies()
        {
            /* Testing of mapping leading up IQueryable
            var studyResource = StudyResource.Generate();
            var study = _mapper.Map<StudyResource, Study>(studyResource);
            var mappedStudyResource = _mapper.Map<Study, StudyResource>(study);

            yield return mappedStudyResource;

            List<Study> studies = new List<Study>() { study };
            var queryableStudies = studies.AsQueryable() as IQueryable;
            foreach(var s in queryableStudies)
                yield return _mapper.Map<Study, StudyResource>(s as Study);
            */

            var results = _queryBus.Query(new GetAllStudiesQuery());
            foreach(var result in results)
                yield return _mapper.Map<Study, StudyResource>(result as Study);
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