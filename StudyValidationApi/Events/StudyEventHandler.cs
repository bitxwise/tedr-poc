using System;
using System.Collections.Generic;
using System.Linq;
using Risly.Cqrs;
using Risly.Cqrs.Kafka;
using StudyApi.Events;
using StudyValidationApi.Models;
using StudyValidationApi.Persistence;
using StudyValidationApi.ValidationRules;

namespace StudyValidationApi.Events
{
    public class StudyEventHandler : IEventHandler
    {
        private readonly Dictionary<Type, Action<Event>> _handlers;

        // TODO: (?)Combine these two as they are combined in StudyApi
        private readonly IStudyRepository _studyRepository;
        private readonly IEventPublisher _eventPublisher;

        // TODO: Separate validation rules into its own object so that they can be managed separately
        private readonly List<IValidationRule> _validationRules;

        public StudyEventHandler(IStudyRepository studyRepository, IEventPublisher eventPublisher)
        {
            _handlers = new Dictionary<Type, Action<Event>>();
            _handlers.Add(typeof(StudyCreatedEvent), (e) => Handle((StudyCreatedEvent)e));
            _handlers.Add(typeof(AccessionNumberChangedEvent), (e) => Handle((AccessionNumberChangedEvent)e));
            _handlers.Add(typeof(StudyReviewedEvent), (e) => Handle((StudyReviewedEvent)e));

            // TODO: Separate validation rules into its own object so that they can be managed separately
            _validationRules = new List<IValidationRule>() {
                new PrefixAccessionNumberValidationRule() {
                    Prefix = "30-",
                    Option = PrefixAccessionNumberValidationRule.PrefixOption.DoesNotStartWith
                }
            };

            _studyRepository = studyRepository;
            _eventPublisher = eventPublisher;
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

            _studyRepository.Save(study);
        }

        public void Handle(StudyReviewedEvent e)
        {
            var study = _studyRepository.GetById(e.StudyId);

            // TODO: Separate validation rules into its own object so that they can be managed separately
            var validationExceptions = _validationRules.SelectMany(vr => vr.Validate(study));
            if(!validationExceptions.Any())
            {
                _eventPublisher.Publish(new StudyAutoValidatedEvent(study.Id));
            }
            else
            {
                // TODO: Abstract relationship between possible validation exceptions
                //       and the exception events they raise.
                var validationException = validationExceptions.FirstOrDefault();
                _eventPublisher.Publish(new BadStudyAccessionEvent(study.Id, study.AccessionNumber, validationException.Message));
            }
        }
    }
}