// TODO: Migrate and change namespace
using System;

namespace FacilityApi.Cqrs
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

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            _storage.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges(), expectedVersion);
        }

        public T GetById(Guid id)
        {
            var obj = new T();//lots of ways to do this
            var e = _storage.GetEventsForAggregate(id);
            obj.LoadsFromHistory(e);
            return obj;
        }
    }
}