using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Risly.Cqrs
{
    /// <summary>
    /// Fake bus used to simulate an external communication bus.
    /// Leveraged from Greg Young's SimpleCQRS approach.
    /// 
    /// Although more than one handler can be register per type,
    /// the logic throws an exception if more tha none handler
    /// is registered per type - haven't figured out how I want
    /// to handle this just yet,,,
    /// </summary>
    public class FakeBus : ICommandBus, IEventPublisher, IQueryBus
    {
        private readonly Dictionary<Type, List<Action<IMessage>>> _commandRoutes = new Dictionary<Type, List<Action<IMessage>>>();

        private readonly Dictionary<Type, List<Func<IMessage, IQueryable>>> _queryRoutes = new Dictionary<Type, List<Func<IMessage, IQueryable>>>();

        public void RegisterCommandHandler<T>(Action<T> handler) where T : IMessage
        {
            List<Action<IMessage>> handlers;

            if(!_commandRoutes.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<IMessage>>();
                _commandRoutes.Add(typeof(T), handlers);
            }

            handlers.Add((x => handler((T)x)));
        }

        public void RegisterQueryHandler<T>(Func<T, IQueryable> handler) where T : IMessage
        {
            List<Func<IMessage, IQueryable>> handlers;

            if(!_queryRoutes.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Func<IMessage, IQueryable>>();
                _queryRoutes.Add(typeof(T), handlers);
            }

            handlers.Add((x => handler((T)x)));
        }

        public void Send<T>(T command) where T : ICommand
        {
            List<Action<IMessage>> handlers;

            if (_commandRoutes.TryGetValue(typeof(T), out handlers))
            {
                if (handlers.Count != 1) throw new InvalidOperationException("cannot send to more than one handler");
                handlers[0](command);
            }
            else
            {
                throw new InvalidOperationException("no handler registered");
            }
        }

        public void Publish<T>(T @event) where T : Event
        {
            List<Action<IMessage>> handlers;

            if (!_commandRoutes.TryGetValue(@event.GetType(), out handlers)) return;

            foreach(var handler in handlers)
            {
                //dispatch on thread pool for added awesomeness
                var handler1 = handler;
                ThreadPool.QueueUserWorkItem(x => handler1(@event));
            }
        }

        public IQueryable Query<T>(T query) where T : IQuery
        {
            List<Func<IMessage, IQueryable>> handlers;

            if (_queryRoutes.TryGetValue(typeof(T), out handlers))
            {
                if (handlers.Count != 1) throw new InvalidOperationException("cannot send to more than one handler");
                return handlers[0](query);
            }
            else
            {
                throw new InvalidOperationException("no handler registered");
            }
        }
    }

    /// <summary>
    /// Represents a handler for, in this case, a command or event.
    /// </summary>
    public interface IHandles<T>
    {
        void Handle(T IMessage);
    }

    /// <summary>
    /// Represents a communication bus that sends commands.
    /// From a pattern perspective, the bus merely passes commands to command handlers.
    /// </summary>
    public interface ICommandBus
    {
        void Send<T>(T command) where T : ICommand;
    }

    /// <summary>
    /// Represents a publisher of events.
    /// From a pattern perspective, the publisher broadcasts events to event handlers.
    /// </summary>
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : Event;
    }

    /// <summary>
    /// Represents a communication bus that sends queries.
    /// From a pattern perspective, the bus merely passes queries to query handlers.
    /// </summary>
    public interface IQueryBus
    {
        IQueryable Query<T>(T query) where T : IQuery;
    }
}