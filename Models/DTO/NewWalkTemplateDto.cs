using System.ComponentModel.DataAnnotations;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO 
{


    public class NewWalkTemplateDTO
    {
        [Required]
        [MinLength(4,ErrorMessage ="Name must be a minimum of 4 characters...")]
        [MaxLength(50,ErrorMessage ="Name must be no longer than 40 characters...")]
        public string Name {get; set;}

        public string Description {get; set;}

        [Required]
        [Range(0,50)]
        public double WalkLengthInKm {get;set;}

        public string? WalkImageUrl {get;set;}

        public Guid DifficultyId {get; set;}

        public Guid RegionId {get;set;}

    }




};