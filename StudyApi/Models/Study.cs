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

        private Guid _facilityId;
        public Guid FacilityId
        {
            get { return _facilityId; }
        }

        private string _facilityName;
        public string FacilityName
        {
            get { return _facilityName; }
        }

        private string _accessionNumber;
        public string AccessionNumber
        {
            get { return _accessionNumber; }
        }

        private Guid _procedureId;
        public Guid ProcedureId
        {
            get { return _procedureId; }
        }

        private string _procedureName;
        public string ProcedureName
        {
            get { return _procedureName; }
        }

        private string _reason;
        public string Reason
        {
            get { return _reason; }
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
        public Study(Guid studyId, Guid facilityId, string facilityName,
            string accessionNumber, Guid procedureId, string procedureName, DateTime procedureDate,
            string reason, Guid imageSetId, string dicomInstanceId, int totalImages, int imagesReceived,
            string patientFirstName, string patientLastName, string patientGender,
                DateTime? patientDateOfBirth, string patientMrn,
            string referringPhysicianFirstName, string referringPhysicianLastName)
        {
            ApplyChange(new StudyCreatedEvent(studyId));
            ApplyChange(new FacilityChangedEvent(studyId, facilityId, facilityName));
            ApplyChange(new AccessionNumberChangedEvent(studyId, accessionNumber));
            ApplyChange(new ProcedureChangedEvent(studyId, procedureId, procedureName));
            ApplyChange(new ReasonChangedEvent(studyId, reason));
            ApplyChange(new ImageSetChangedEvent(studyId, imageSetId, dicomInstanceId));
            ApplyChange(new ImagesReceivedEvent(studyId, imagesReceived));
            ApplyChange(new ImageTotalChangedEvent(studyId, totalImages));
            ApplyChange(new PatientChangedEvent(studyId, patientMrn, patientFirstName, patientLastName, patientGender, patientDateOfBirth));
            ApplyChange(new ReferringPhysicianChangedEvent(studyId, referringPhysicianFirstName, referringPhysicianLastName));
        }

        public void Apply(StudyCreatedEvent e)
        {
            _id = e.StudyId;
        }

        public void Apply(FacilityChangedEvent e)
        {
            _facilityId = e.FacilityId;
            _facilityName = e.FacilityName;
        }

        public void Apply(AccessionNumberChangedEvent e)
        {
            _accessionNumber = e.AccessionNumber;
        }

        public void Apply(ProcedureChangedEvent e)
        {
            _procedureId = e.ProcedureId;
            _procedureName = e.ProcedureName;
        }

        public void Apply(ReasonChangedEvent e)
        {
            _reason = e.Reason;
        }

        public void Apply(ImageSetChangedEvent e)
        {
            // Intentionally left blank
        }

        public void Apply(ImagesReceivedEvent e)
        {
            // Intentionally left blank
        }

        public void Apply(ImageTotalChangedEvent e)
        {
            // Intentionally left blank
        }

        public void Apply(PatientChangedEvent e)
        {
            // Intentionally left blank
        }

        public void Apply(ReferringPhysicianChangedEvent e)
        {
            // Intentionally left blank
        }
    }
}