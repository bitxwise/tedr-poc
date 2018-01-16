using System;
using Risly.Cqrs;

namespace StudyValidationApi.Events
{
    public class AccessionNumberChangedEvent : Event
    {
        public readonly Guid StudyId;
        public readonly string AccessionNumber;

        public AccessionNumberChangedEvent(Guid studyId, string accessionNumber)
        {
            StudyId = studyId;
            AccessionNumber = accessionNumber;
        }
    }
}