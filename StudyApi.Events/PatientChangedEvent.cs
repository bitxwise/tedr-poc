using System;
using Risly.Cqrs;

namespace StudyApi.Events
{
    public class PatientChangedEvent : Event
    {
        public readonly Guid StudyId;
        public readonly string PatientMrn;
        public readonly string PatientFirstName;
        public readonly string PatientLastName;
        public readonly string PatientGender;
        public readonly DateTime? PatientDateOfBirth;

        public PatientChangedEvent(Guid studyId, string patientMrn, string patientFirstName, string patientLastName, string patientGender, DateTime? patientDateOfBirth)
        {
            StudyId = studyId;
            PatientMrn = patientMrn;
            PatientFirstName = patientFirstName;
            PatientLastName = patientLastName;
            PatientGender = patientGender;
        }
    }
}