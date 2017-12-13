namespace FacilityApi.Cqrs
{
    public class FakeBus
    {
         
    }

    /// <summary>
    /// Represents a communication bus that sends commands.
    /// From a pattern perspective, the bus merely passes commands to command handlers.
    /// </summary>
    public interface ICommandBus
    {
        void Send<T>(T command) where T : ICommand;
    }
}