using System;
using Risly.Cqrs;

namespace StudyValidationApi.Events
{
    /// <summary>
    /// Represents event that is raised when a study is reviewed.
    /// 
    /// If this were not a POC, would capture an actual study review,
    /// not to be confused with study report review from quality peer review.
    /// </summary>
    public class StudyReviewedEvent : Event
    {
        public readonly Guid StudyId;

        public StudyReviewedEvent(Guid studyId)
        {
            StudyId = studyId;
        }
    }
}