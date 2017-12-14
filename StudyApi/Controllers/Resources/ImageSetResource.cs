using System;

namespace StudyApi.Controllers.Resources
{
    public class ImageSetResource
    {
        // Internal identifier
        public Guid Id { get; set; }
        
        // Externally provided identifier
        public string DicomInstanceId { get; set; }

        public int TotalImages { get; set; }

        public int ImagesReceived { get; set; }
    }
}