using System;
using Risly.Cqrs;

namespace StudyApi.Events
{
    public class BadStudyAccessionEvent : Event
    {
        public readonly Guid StudyId;
        public readonly string AccessionNumber;
        public readonly string Message;

        public BadStudyAccessionEvent(Guid studyId, string accessionNumber, string message)
        {
            StudyId = studyId;
            AccessionNumber = accessionNumber;
            Message = message;
        }
    }
}