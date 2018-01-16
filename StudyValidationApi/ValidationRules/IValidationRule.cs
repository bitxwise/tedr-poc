using System.Collections.Generic;
using StudyValidationApi.Models;

namespace StudyValidationApi.ValidationRules
{
    public interface IValidationRule
    {
        IEnumerable<ValidationException> Validate(Study study);
    }
}