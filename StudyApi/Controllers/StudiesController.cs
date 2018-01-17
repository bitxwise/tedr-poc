using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            // TODO: Clean up null checking to not duplicate checks
            Guid facilityId = studyResource.Facility != null ? studyResource.Facility.Id : Guid.Empty;
            string facilityName = studyResource.Facility != null ? studyResource.Facility.Name : string.Empty;
            Guid procedureId = studyResource.Procedure != null ? studyResource.Procedure.Id : Guid.Empty;
            string procedureName = studyResource.Procedure != null ? studyResource.Procedure.Name : string.Empty;
            Guid imageSetId = studyResource.ImageSet != null ? studyResource.ImageSet.Id : Guid.Empty;
            string dicomInstanceId = studyResource.ImageSet != null ? studyResource.ImageSet.DicomInstanceId : string.Empty;
            int totalImages = studyResource.ImageSet != null ? studyResource.ImageSet.TotalImages : 0;
            string patientFirstName = studyResource.Patient != null ? studyResource.Patient.FirstName : string.Empty;
            string patientLastName = studyResource.Patient != null ? studyResource.Patient.LastName : string.Empty;
            string patientGender = studyResource.Patient != null ? studyResource.Patient.Gender : string.Empty;
            string patientMrn = studyResource.Patient != null ? studyResource.Patient.Mrn : string.Empty;
            DateTime? patientDateOfBirth = studyResource.Patient != null ? studyResource.Patient.DateOfBirth : null;
            string referringPhysicianFirstName = studyResource.ReferringPhysician != null ? studyResource.ReferringPhysician.FirstName : string.Empty;
            string referringPhysicianLastName = studyResource.ReferringPhysician != null ? studyResource.ReferringPhysician.LastName : string.Empty;

            var command = new CreateStudyCommand(studyId, facilityId, facilityName,
                studyResource.AccessionNumber, procedureId, procedureName,
                studyResource.ProcedureDate, studyResource.Reason, imageSetId, dicomInstanceId,
                totalImages, totalImages, patientFirstName, patientLastName, patientGender, patientDateOfBirth,
                patientMrn, referringPhysicianFirstName,referringPhysicianLastName);

            Task.Run(() => _commandBus.Send(command));
            
            return Ok(studyId.ToString() + " created");
        }

        [HttpPost, Route("api/studies/review")]
        public IActionResult ReviewStudy([FromBody]Guid studyId)
        {
            var command = new ReviewStudyCommand(studyId);
            
            Task.Run(() => _commandBus.Send(command));

            return Ok(studyId.ToString() + " reviewed");
        }
    }
}