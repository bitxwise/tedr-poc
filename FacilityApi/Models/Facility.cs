using System;
using FacilityApi.Events;
using Risly.Cqrs;

namespace FacilityApi.Models
{
    public class Facility : AggregateRoot
    {
        private Guid _id;
        public override Guid Id
        {
            get { return _id; }
        }

        // TODO: Protect constructor from public abuse
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