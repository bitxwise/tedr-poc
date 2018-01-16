using System;
using Risly.Cqrs;

namespace StudyApi.Commands
{
    public class CreateStudyCommand : ICommand
    {
        public readonly Guid StudyId;
        public readonly Guid FacilityId;
        public readonly string FacilityName;
        public readonly string AccessionNumber;
        public readonly Guid ProcedureId;
        public readonly string ProcedureName;
        public readonly DateTime ProcedureDate;
        public readonly string Reason;
        public readonly Guid ImageSetId;
        public readonly string DicomInstanceId;
        public readonly int TotalImages;
        public readonly int ImagesReceived;
        public readonly string PatientFirstName;
        public readonly string PatientLastName;
        public readonly string PatientGender;
        public readonly DateTime? PatientDateOfBirth;
        public readonly string PatientMrn;
        public string ReferringPhysicianFirstName;
        public string ReferringPhysicianLastName;

        public CreateStudyCommand(Guid studyId, Guid facilityId, string facilityName, string accessionNumber, Guid procedureId, string procedureName, DateTime procedureDate,
            string reason, Guid imageSetId, string dicomInstanceId, int totalImages, int imagesReceived, string patientFirstName, string patientLastName, string patientGender,
            DateTime? patientDateOfBirth, string patientMrn, string referringPhysicianFirstName, string referringPhysicianLastName)
        {
            StudyId = studyId;
            FacilityId = facilityId;
            FacilityName = facilityName;
            AccessionNumber = accessionNumber;
            ProcedureId = procedureId;
            ProcedureName = procedureName;
            ProcedureDate = procedureDate;
            Reason = reason;
            ImageSetId = imageSetId;
            DicomInstanceId = dicomInstanceId;
            TotalImages = totalImages;
            ImagesReceived = imagesReceived;
            PatientFirstName = patientFirstName;
            PatientLastName = patientLastName;
            PatientGender = patientGender;
            PatientDateOfBirth = patientDateOfBirth;
            PatientMrn = patientMrn;
            ReferringPhysicianFirstName = referringPhysicianFirstName;
            ReferringPhysicianLastName = referringPhysicianLastName;
        }
    }
}