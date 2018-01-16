using System;
using System.Collections.Generic;
using System.Linq;
using Risly.Cqrs;
using Risly.Cqrs.Kafka;
using StudyValidationApi.Models;
using StudyValidationApi.Persistence;
using StudyValidationApi.ValidationRules;

namespace StudyValidationApi.Events
{
    public class StudyEventHandler : IEventHandler
    {
        private readonly Dictionary<Type, Action<Event>> _handlers;
        private readonly StudyRepository _studyRepository;

        // TODO: Separate validation rules into its own object so that they can be managed separately
        private readonly List<IValidationRule> _validationRules;

        public StudyEventHandler(StudyRepository studyRepository)
        {
            _handlers = new Dictionary<Type, Action<Event>>();

            // TODO: Separate validation rules into its own object so that they can be managed separately
            _validationRules = new List<IValidationRule>() {
                new PrefixAccessionNumberValidationRule() {
                    Prefix = "30-",
                    Option = PrefixAccessionNumberValidationRule.PrefixOption.DoesNotStartWith
                }
            };

            _studyRepository = studyRepository;
        }

        public void Handle(Event @event)
        {
            Type eventType = @event.GetType();
            
            if(_handlers.ContainsKey(eventType))
                _handlers[eventType](@event);
        }

        public void Handle(StudyCreatedEvent e)
        {
            var study = new Study();
            study.Apply(e);

            _studyRepository.Save(study);
        }

        public void Handle(AccessionNumberChangedEvent e)
        {
            var study = _studyRepository.GetById(e.StudyId);
            study.Apply(e);
        }

        public void Handle(StudyReviewedEvent e)
        {
            var study = _studyRepository.GetById(e.StudyId);

            // TODO: Separate validation rules into its own object so that they can be managed separately
            var validationExceptions = _validationRules.SelectMany(vr => vr.Validate(study));
            if(!validationExceptions.Any())
            {
                // TODO: Publish StudyAutoValidated event
            }
            else
            {
                // TODO: Publish StudyAccessionException event
            }
        }
    }
}