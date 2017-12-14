// TODO: Migrate and change namespace
using System;
using System.Collections.Generic;
using System.Threading;

namespace FacilityApi.Cqrs
{
    /// <summary>
    /// Fake bus used to simulate an external communication bus.
    /// Leveraged from Greg Young's SimpleCQRS approach.
    /// </summary>
    public class FakeBus : ICommandBus, IEventPublisher
    {
        private readonly Dictionary<Type, List<Action<IMessage>>> _routes = new Dictionary<Type, List<Action<IMessage>>>();

        public void RegisterHandler<T>(Action<T> handler) where T : IMessage
        {
            List<Action<IMessage>> handlers;

            if(!_routes.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<IMessage>>();
                _routes.Add(typeof(T), handlers);
            }

            handlers.Add((x => handler((T)x)));
        }

        public void Send<T>(T command) where T : ICommand
        {
            List<Action<IMessage>> handlers;

            if (_routes.TryGetValue(typeof(T), out handlers))
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

            if (!_routes.TryGetValue(@event.GetType(), out handlers)) return;

            foreach(var handler in handlers)
            {
                //dispatch on thread pool for added awesomeness
                var handler1 = handler;
                ThreadPool.QueueUserWorkItem(x => handler1(@event));
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
}