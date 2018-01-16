using System;
using Risly.Cqrs;

namespace StudyValidationApi.Events
{
    /// <summary>
    /// Represents an event raised when a new study is created.
    /// </summary>
    public class StudyCreatedEvent : Event
    {
        public readonly Guid StudyId;

        public StudyCreatedEvent(Guid studyId)
        {
            StudyId = studyId;
        }
    }
}