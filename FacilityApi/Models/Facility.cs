using System;
using FacilityApi.Cqrs;
using FacilityApi.Events;

namespace FacilityApi.Models
{
    public class Facility : AggregateRoot
    {
        private Guid _id;
        public override Guid Id
        {
            get { return _id; }
        }

        // TODO: Make constructor non-private
        /// <summary>
        /// Initializes a new instance of the FacilityApi.Models.Facility class.
        /// This constructor is intended to be used by repositories only.
        /// </summary>
        public Facility()
        {
            // Intentionally left blank
        }

        /// <summary>
        /// Initializes a new instance of the FacilityApi.Models.Facility class
        /// with the specified ID and name.
        /// </summary>
        /// <param name="facilityId">Unique facility identifier.</param>
        /// <param name="facilityName">Facility name.</param>
        public Facility(Guid facilityId, string facilityName)
        {
            ApplyChange(new FacilityCreatedEvent(facilityId, facilityName));
        }

        public void Apply(FacilityCreatedEvent e)
        {
            _id = e.FacilityId;
        }
    }
}