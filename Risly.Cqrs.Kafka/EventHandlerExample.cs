using System;
using System.Collections.Generic;

namespace Risly.Cqrs.Kafka
{
    public class EventHandlerExample : IEventHandler
    {
        private readonly Dictionary<Type, Action<Event>> _handlers;
        //private readonly IRepository _Repository;

        EventHandlerExample()//IRepository Repository)
        {
            _handlers = new Dictionary<Type, Action<Event>>();
            //_handlers.Add(typeof(CreatedEvent), (e) => Handle((CreatedEvent)e));

            //_Repository = Repository;
        }

        public void Handle(Event Event)
        {
            Type eventType = Event.GetType();
            
            if(_handlers.ContainsKey(eventType))
                _handlers[eventType](Event);
        }
    }
}