using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{

    public class UpdateRegionTemplateDto
    {
        [Required]
        [MinLength(3, ErrorMessage="Code has to be a minimum of three characters...")]
        [MaxLength(3, ErrorMessage ="Code has to be a maximum of three characters...")]
        public string Code {get;set;}

        [Required]
        [MaxLength(3, ErrorMessage ="Name must be equal or less than 100 characters...")]
        public string Name {get; set;}

        public string? RegionImageUrl {get; set;}
    }

}



