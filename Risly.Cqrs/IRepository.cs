using System;

namespace Risly.Cqrs
{
    /// <summary>
    /// Represents a repository for domain aggregates.
    /// </summary>
    public interface IRepository<T> where T : AggregateRoot
    {
        void Save(T aggregate, int expectedVersion);
        T GetById(Guid id);
    }
}