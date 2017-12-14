using System;

namespace Risly.Cqrs
{
    /// <summary>
    /// Represents a repository for domain aggregates.
    /// </summary>
    public interface IRepository<T>
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        T GetById(Guid id);
    }
}