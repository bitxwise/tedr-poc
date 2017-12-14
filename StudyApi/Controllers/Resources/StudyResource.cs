using System;

namespace StudyApi.Controllers.Resources
{
    public class StudyResource
    {
        public Guid Id { get; set; }

        public string AccessionNumber { get; set; }

        public NamedResource Facility { get; set; }

        public NamedResource Procedure { get; set; }

        public DateTime ProcedureDate { get; set; }

        public string Reason { get; set; }

        public ImageSetResource ImageSet { get; set; }

        public PatientResource Patient { get; set; }

        public PersonResource ReferringPhysician { get; set; }

        public NamedResource StudyStatus { get; set; }

        public static StudyResource Generate()
        {
            return new StudyResource() {
                    Id = Guid.NewGuid(),
                    AccessionNumber = "SG" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Facility = new NamedResource() {
                        Id =  Guid.NewGuid(),
                        Name = "Seattle Grace"
                    },
                    Procedure = new NamedResource() {
                        Id = Guid.NewGuid(),
                        Name = "CT HEAD +"
                    },
                    ProcedureDate = DateTime.Now,
                    Reason = "Pain ~ Fall",
                    ImageSet = new ImageSetResource() {
                        Id = Guid.NewGuid(),
                        DicomInstanceId = "SOME-PATIENT-STUDY-ID",
                        TotalImages = 500,
                        ImagesReceived = 500
                    },
                    Patient = new PatientResource() {
                        FirstName = "John",
                        LastName = "Doe",
                        Gender = "M",
                        DateOfBirth = DateTime.Today.AddYears(-33),
                        Mrn = "ABC123789"
                    },
                    ReferringPhysician = new PersonResource() {
                        FirstName = "Greg",
                        LastName = "House",
                    },
                    StudyStatus = new NamedResource() {
                        Id = Guid.NewGuid(),
                        Name = "Ready"
                    }
                };
        }
    }
}