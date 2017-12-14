using System;
using System.Collections.Generic;

namespace Risly.Cqrs
{
    /// <summary>
    /// Represents an event store.
    /// </summary>
    public interface IEventStore
    {
         void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
        List<Event> GetEventsForAggregate(Guid aggregateId);
    }
}