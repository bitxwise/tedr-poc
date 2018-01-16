using System;
using Risly.Cqrs;

namespace StudyApi.Events
{
    public class ReasonChangedEvent : Event
    {
        public readonly Guid StudyId;
        public readonly string Reason;

        public ReasonChangedEvent(Guid studyId, string reason)
        {
            StudyId = studyId;
            Reason = reason;
        }
    }
}