using System;
using Risly.Cqrs;

namespace StudyApi.Events
{
    public class ProcedureChangedEvent : Event
    {
        public readonly Guid StudyId;
        public readonly Guid ProcedureId;
        public readonly string ProcedureName;

        public ProcedureChangedEvent(Guid studyId, Guid procedureId, string procedureName)
        {
            StudyId = studyId;
            ProcedureId = procedureId;
            ProcedureName = procedureName;
        }
    }
}