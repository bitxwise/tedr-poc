using Risly.Cqrs;
using StudyApi.Models;

namespace StudyApi.Commands
{
    /// <summary>
    /// Collection of handlers for study commands.
    /// </summary>
    public class StudyCommandHandlers
    {
        private readonly IRepository<Study> _repository;
        
        /// <summary>
        /// Initializes a new instance of the StudyApi.Commands.StudyCommandHandlers class
        /// with the specified study repository.
        /// </summary>
        /// <param name="repository">Repository for studies.</param>
        public StudyCommandHandlers(IRepository<Study> repository)
        {
            _repository = repository;
        }
        
        /// <summary>
        /// Handles the specified create study command.
        /// </summary>
        /// <param name="command">The create study command to handle.</param>
        public void Handle(CreateStudyCommand command)
        {
            var study = new Study(command.StudyId, command.FacilityId, command.FacilityName, command.AccessionNumber,
                command.ProcedureId, command.ProcedureName, command.ProcedureDate, command.Reason,
                command.ImageSetId, command.DicomInstanceId, command.TotalImages, command.ImagesReceived,
                command.PatientFirstName, command.PatientLastName, command.PatientGender, command.PatientDateOfBirth, command.PatientMrn,
                command.ReferringPhysicianFirstName, command.ReferringPhysicianLastName);
            
            _repository.Save(study, -1);
        }

        public void Handle(ReviewStudyCommand command)
        {
            var study = _repository.GetById(command.StudyId);
            
            // If not POC, would capture actual study review, not to be confused with study report review from quality peer review
            study.CaptureReview();

            _repository.Save(study, study.Version - 1);
        }
    }
}