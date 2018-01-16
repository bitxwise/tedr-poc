using System;
using Risly.Cqrs;
using StudyApi.Events;
using StudyValidationApi.Events;

namespace StudyValidationApi.Models
{
    public class Study : AggregateRoot
    {
        private Guid _id;
        public override Guid Id
        {
            get { return _id; }
        }

        private string _accessionNumber;
        public string AccessionNumber
        {
            get { return _accessionNumber; }
        }

        // TODO: Protect constructor from public abuse
        /// <summary>
        /// Initializes a new instance of the StudyValidationApi.Models.Study class.
        /// This constructor is intended to be used by repositories only.
        /// </summary>
        public Study()
        {
            // Intentionally left blank
        }

        #region Event Applications

        public void Apply(StudyCreatedEvent e)
        {
            _id = e.StudyId;
        }

        public void Apply(AccessionNumberChangedEvent e)
        {
            _accessionNumber = e.AccessionNumber;
        }

        #endregion Event Applications
    }
}