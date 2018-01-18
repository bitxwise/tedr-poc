using System;
using System.Collections.Generic;
using Risly.Cqrs;
using Risly.Cqrs.Kafka;
using StudyApi.Events;
using StudyIssuesApi.Models;
using StudyIssuesApi.Persistence;

namespace StudyIssuesApi.Events
{
    /// <summary>
    /// Handles study events.
    /// 
    /// Configured to only handle study events from other microservices via handling registration.
    /// </summary>
    public class StudyEventHandler : IEventHandler
    {
        private readonly Dictionary<Type, Action<Event>> _handlers;
        private readonly ITicketRepository _ticketRepository;

        public StudyEventHandler(ITicketRepository ticketRepository)
        {
            _handlers = new Dictionary<Type, Action<Event>>();
            _handlers.Add(typeof(BadStudyAccessionEvent), (e) => Handle((BadStudyAccessionEvent)e));

            _ticketRepository = ticketRepository;
        }

        public void Handle(Event @event)
        {
            Type eventType = @event.GetType();
            
            if(_handlers.ContainsKey(eventType))
                _handlers[eventType](@event);
        }

        public void Handle(BadStudyAccessionEvent e)
        {
            Ticket ticket = new Ticket() {
                Id = Guid.NewGuid(),
                Description = e.Message
            };
            
            _ticketRepository.AddTicket(ticket);
        }
    }
}