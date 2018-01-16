using System;
using Risly.Cqrs;
using StudyApi.Events;

namespace StudyApi.Models
{
    public class Study : AggregateRoot
    {
        private Guid _id;
        public override Guid Id
        {
            get { return _id; }
        }

        // TODO: Protect constructor from public abuse
        /// <summary>
        /// Initializes a new instance of the StudyApi.Models.Study class.
        /// This constructor is intended to be used by repositories only.
        /// </summary>
        public Study()
        {
            // Intentionally left blank
        }

        /// <summary>
        /// Initializes a new instance of the StudyApi.Models.Study class
        /// with the specified ID and attributes.
        /// </summary>
        public Study(Guid studyId, Guid facilityId, string facilityName, string accessionNumber, Guid procedureId, string procedureName, DateTime procedureDate,
            string reason, Guid imageSetId, string dicomInstanceId, int totalImages, int imagesReceived, string patientFirstName, string patientLastName, string patientGender,
            DateTime? patientDateOfBirth, string patientMrn, string referringPhysicianFirstName, string referringPhysicianLastName)
        {
            ApplyChange(new StudyCreatedEvent(studyId));
        }

        public void Apply(StudyCreatedEvent e)
        {
            _id = e.StudyId;
        }
    }
}