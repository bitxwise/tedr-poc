using System;
using Risly.Cqrs;

namespace StudyApi.Events
{
    public class StudyAutoValidatedEvent : Event
    {
        public readonly Guid StudyId;

        public StudyAutoValidatedEvent(Guid studyId)
        {
            StudyId = studyId;
        }
    }
}