using System;
using System.Collections.Generic;
using Risly.Cqrs;
using Risly.Cqrs.Kafka;
using StudyApi.Models;

namespace StudyApi.Events
{
    /// <summary>
    /// Handles study events.
    /// 
    /// Configured to only handle study events from other microservices via handling registration.
    /// </summary>
    public class StudyEventHandler : IEventHandler
    {
        private readonly Dictionary<Type, Action<Event>> _handlers;
        private readonly IRepository<Study> _studyRepository;

        public StudyEventHandler(IRepository<Study> studyRepository)
        {
            _handlers = new Dictionary<Type, Action<Event>>();
            _handlers.Add(typeof(StudyAutoValidatedEvent), (e) => Handle((StudyAutoValidatedEvent)e));
            _handlers.Add(typeof(BadStudyAccessionEvent), (e) => Handle((BadStudyAccessionEvent)e));

            _studyRepository = studyRepository;
        }

        public void Handle(Event @event)
        {
            Type eventType = @event.GetType();
            
            if(_handlers.ContainsKey(eventType))
                _handlers[eventType](@event);
        }

        public void Handle(StudyAutoValidatedEvent e)
        {
            var study = _studyRepository.GetById(e.StudyId);
            study.Apply(e);

            _studyRepository.Save(study, study.Version - 1);
        }

        public void Handle(BadStudyAccessionEvent e)
        {
            var study = _studyRepository.GetById(e.StudyId);
            study.Apply(e);

            _studyRepository.Save(study, study.Version - 1);
        }
    }
}