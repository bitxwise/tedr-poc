using System;
using FacilityApi.Cqrs;

namespace FacilityApi.Events
{
    /// <summary>
    /// Represents an event raised when a new facility is created.
    /// </summary>
    public class FacilityCreatedEvent : Event
    {
        public readonly Guid FacilityId;
        public readonly string FacilityName;

        public FacilityCreatedEvent(Guid facilityId, string facilityName)
        {
            FacilityId = facilityId;
            FacilityName = facilityName;
        }
    }
}