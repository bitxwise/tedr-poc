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
    }
}