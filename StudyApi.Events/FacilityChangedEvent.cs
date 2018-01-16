using System;
using Risly.Cqrs;

namespace StudyApi.Events
{
    public class FacilityChangedEvent : Event
    {
        public readonly Guid StudyId;
        public readonly Guid FacilityId;
        public readonly string FacilityName;

        public FacilityChangedEvent(Guid studyId, Guid facilityId, string facilityName)
        {
            StudyId = studyId;
            FacilityId = facilityId;
            FacilityName = facilityName;
        }
    }
}