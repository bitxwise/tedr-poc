using System.ComponentModel.DataAnnotations;

namespace FacilityApi.Controllers.Resources
{
    public class CreateFacilityResource
    {
        [Required, MaxLength(255)]
        public string FacilityName { get; set; }
    }
}