using System;

namespace StudyApi.Controllers.Resources
{
    public class PatientResource : PersonResource
    {
        public string Mrn { get; set; }
    }
}