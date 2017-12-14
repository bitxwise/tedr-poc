using System;

namespace StudyApi.Controllers.Resources
{
    public class PersonResource
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}