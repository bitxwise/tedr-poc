using System;
using Risly.Cqrs;

namespace StudyApi.Commands
{
    public class ReviewStudyCommand : ICommand
    {
        public readonly Guid StudyId;

        public ReviewStudyCommand(Guid studyId)
        {
            StudyId = studyId;
        }
    }
}