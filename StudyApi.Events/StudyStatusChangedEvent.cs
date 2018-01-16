using System;
using Risly.Cqrs;

namespace StudyApi.Events
{
    public class StudyStatusChangedEvent : Event
    {
        public readonly Guid StudyId;
        public readonly Guid StatusId;
        public readonly string StatusName;

        public StudyStatusChangedEvent(Guid studyId, Guid statusId, string statusName)
        {
            StudyId = studyId;
            StatusId = statusId;
            StatusName = statusName;
        }
    }
}