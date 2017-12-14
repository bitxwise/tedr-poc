using System;
using Risly.Cqrs;

namespace FacilityApi.Commands
{
    public class CreateFacilityCommand : ICommand
    {
        public readonly Guid FacilityId;
        public readonly string FacilityName;

        public CreateFacilityCommand(Guid facilityId, string facilityName)
        {
            FacilityId = facilityId;
            FacilityName = facilityName;
        }
    }
}