using FacilityApi.Cqrs;

namespace FacilityApi.Commands
{
    /// <summary>
    /// Collection of handlers for facility commands.
    /// </summary>
    public class FacilityCommandHandlers
    {
        /// <summary>
        /// Initializes a new instance of the FacilityApi.Commands.FacilityCommandHandlers class
        /// with the specified facility repository.
        /// </summary>
        /// <param name="repository"></param>
        public FacilityCommandHandlers()
        {
            
        }

        /// <summary>
        /// Handles the specified create facility command.
        /// </summary>
        /// <param name="command">The create facility command to handle.</param>
        public void Handle(CreateFacilityCommand command)
        {
            
        }
    }
}