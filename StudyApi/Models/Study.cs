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

        private Guid _statusId;
        public Guid StatusId
        {
            get { return _statusId; }
        }

        private string _statusName;
        public string StatusName
        {
            get { return _statusName; }
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
            
            if(facilityId != Guid.Empty) ApplyChange(new FacilityChangedEvent(studyId, facilityId, facilityName));
            if(!string.IsNullOrWhiteSpace(accessionNumber)) ApplyChange(new AccessionNumberChangedEvent(studyId, accessionNumber));
            if(procedureId != Guid.Empty) ApplyChange(new ProcedureChangedEvent(studyId, procedureId, procedureName));
            if(!string.IsNullOrWhiteSpace(reason)) ApplyChange(new ReasonChangedEvent(studyId, reason));
            if(imageSetId != Guid.Empty) ApplyChange(new ImageSetChangedEvent(studyId, imageSetId, dicomInstanceId));
            if(imagesReceived > 0) ApplyChange(new ImagesReceivedEvent(studyId, imagesReceived));
            if(totalImages > 0) ApplyChange(new ImageTotalChangedEvent(studyId, totalImages));
            if(!string.IsNullOrWhiteSpace(patientMrn)) ApplyChange(new PatientChangedEvent(studyId, patientMrn, patientFirstName, patientLastName, patientGender, patientDateOfBirth));
            if(!string.IsNullOrWhiteSpace(referringPhysicianLastName)) ApplyChange(new ReferringPhysicianChangedEvent(studyId, referringPhysicianFirstName, referringPhysicianLastName));
        }

        #region Event Applications

        public void Apply(StudyCreatedEvent e)
        {
            _id = e.StudyId;
            _statusId = StudyStatus.CreatedGuid;
            _statusName = StudyStatus.CreatedName;
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
            // Intentionally left blank - internal model doesn't care
        }

        public void Apply(ImagesReceivedEvent e)
        {
            // Intentionally left blank - internal model doesn't care
        }

        public void Apply(ImageTotalChangedEvent e)
        {
            // Intentionally left blank - internal model doesn't care
        }

        public void Apply(PatientChangedEvent e)
        {
            // Intentionally left blank - internal model doesn't care
        }

        public void Apply(ReferringPhysicianChangedEvent e)
        {
            // Intentionally left blank - internal model doesn't care
        }

        public void Apply(StudyStatusChangedEvent e)
        {
            _statusId = e.StatusId;
            _statusName = e.StatusName;
        }

        public void Apply(StudyReviewedEvent e)
        {
            ApplyChange(new StudyStatusChangedEvent(Id, StudyStatus.ReviewedGuid, StudyStatus.ReviewedName));
        }

        public void Apply(StudyAutoValidatedEvent e)
        {
            ApplyChange(new StudyStatusChangedEvent(Id, StudyStatus.ReadyGuid, StudyStatus.ReadyName));
        }

        public void Apply(BadStudyAccessionEvent e)
        {
            ApplyChange(new StudyStatusChangedEvent(Id, StudyStatus.NotReadyGuid, StudyStatus.NotReadyName));
        }

        #endregion Event Applications

        /// <summary>
        /// Captures that study has been reviewed. If this were not a POC,
        /// would specify an actual study review - not to be confused with study report review
        /// from quality peer review.
        /// </summary>
        public void CaptureReview()
        {
            ApplyChange(new StudyReviewedEvent(Id));
        }
    }
}