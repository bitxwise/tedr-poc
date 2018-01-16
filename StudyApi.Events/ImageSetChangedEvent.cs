using System;
using Risly.Cqrs;

namespace StudyApi.Events
{
    public class ImageSetChangedEvent : Event
    {
        public readonly Guid StudyId;
        public readonly Guid ImageSetId;
        public readonly string DicomInstanceId;

        public ImageSetChangedEvent(Guid studyId, Guid imageSetId, string dicomInstanceId)
        {
            StudyId = studyId;
            ImageSetId = imageSetId;
            DicomInstanceId = dicomInstanceId;
        }
    }
}