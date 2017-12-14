// TODO: Migrate and change namespace
namespace FacilityApi.Cqrs
{
    /// <summary>
    /// Marker for events used with CQRS.
    /// </summary>
    public class Event : IMessage
    {
        public int Version { get; set; }
    }
}