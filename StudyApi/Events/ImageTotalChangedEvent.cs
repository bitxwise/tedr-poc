using System;
using Risly.Cqrs;

namespace StudyApi.Events
{
    public class ImageTotalChangedEvent : Event
    {
        public readonly Guid StudyId;
        public readonly int TotalImages;

        public ImageTotalChangedEvent(Guid studyId, int totalImages)
        {
            StudyId = studyId;
            TotalImages = totalImages;
        }
    }
}