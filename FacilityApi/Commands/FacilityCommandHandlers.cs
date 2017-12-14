using FacilityApi.Models;
using Risly.Cqrs;

namespace FacilityApi.Commands
{
    /// <summary>
    /// Collection of handlers for facility commands.
    /// </summary>
    public class FacilityCommandHandlers
    {
        private readonly IRepository<Facility> _repository;
        
        /// <summary>
        /// Initializes a new instance of the FacilityApi.Commands.FacilityCommandHandlers class
        /// with the specified facility repository.
        /// </summary>
        /// <param name="repository">Repository for facilities.</param>
        public FacilityCommandHandlers(IRepository<Facility> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles the specified create facility command.
        /// </summary>
        /// <param name="command">The create facility command to handle.</param>
        public void Handle(CreateFacilityCommand command)
        {
            var facility = new Facility(command.FacilityId, command.FacilityName);
            _repository.Save(facility, -1);
        }
    }
}