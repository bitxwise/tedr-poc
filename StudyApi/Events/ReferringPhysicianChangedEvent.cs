using System;
using Risly.Cqrs;

namespace StudyApi.Events
{
    public class ReferringPhysicianChangedEvent : Event
    {
        public readonly Guid StudyId;
        public readonly string ReferringPhysicianFirstName;
        public readonly string ReferringPhysicianLastName;

        public ReferringPhysicianChangedEvent(Guid studyId, string referringPhysicianFirstName, string referringPhysicianLastName)
        {
            StudyId = studyId;
            ReferringPhysicianFirstName = referringPhysicianFirstName;
            ReferringPhysicianLastName = referringPhysicianLastName;
        }
    }
}