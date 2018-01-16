using System;
using Risly.Cqrs;

namespace StudyApi.Events
{
    public class ImagesReceivedEvent : Event
    {
        public readonly Guid StudyId;
        public readonly int ImagesReceived;

        public ImagesReceivedEvent(Guid studyId, int imagesReceived)
        {
            StudyId = studyId;
            ImagesReceived = imagesReceived;
        }
    }
}