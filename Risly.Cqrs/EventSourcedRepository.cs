using System;

namespace Risly.Cqrs
{
    /// <summary>
    /// Reperesents a repository for domain aggregates that support event sourcing.
    /// Leveraged from Greg Young's SimpleCQRS approach.
    /// </summary>
    /// <returns></returns>
    public class EventSourcedRepository<T> : IRepository<T> where T: AggregateRoot, new()
    {
        private readonly IEventStore _storage;

        public EventSourcedRepository(IEventStore storage)
        {
            _storage = storage;
        }

        public void Save(T aggregate, int expectedVersion)
        {
            _storage.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges(), expectedVersion);
        }

        public T GetById(Guid id)
        {
            var obj = new T();//lots of ways to do this
            var e = _storage.GetEventsForAggregate(id);
            obj.LoadFromHistory(e);
            return obj;
        }
    }
}