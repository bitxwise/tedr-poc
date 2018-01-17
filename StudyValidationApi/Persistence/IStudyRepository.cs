using System;
using StudyValidationApi.Models;

namespace StudyValidationApi.Persistence
{
    public interface IStudyRepository
    {
         Study GetById(Guid id);
         void Save(Study study);
    }
}