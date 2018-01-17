using System;
using System.Collections.Generic;
using Risly.Cqrs;
using StudyValidationApi.Models;

namespace StudyValidationApi.Persistence
{
    public class StudyRepository : IStudyRepository
    {
        private readonly Dictionary<Guid, Study> _studies = new Dictionary<Guid, Study>();

        public Study GetById(Guid id)
        {
            if(!_studies.ContainsKey(id))
                _studies.Add(id, new Study());
            
            return _studies[id];
        }

        public void Save(Study study)
        {
            // optimistically assumes all studies that are modified after creation are from this repository
            if(!_studies.ContainsKey(study.Id))
                _studies.Add(study.Id, study);
        }
    }
}