using NZWalks.API.Models;
using System.ComponentModel.DataAnnotations;



namespace NZWalks.API.Models.DTO
{
public class UpdateWalkTemplateDto{
    
    [Required]
    [MinLength(4,ErrorMessage ="Name must be a minimum of 4 characters...")]
    [MaxLength(50,ErrorMessage ="Name must be no longer than 40 characters...")]
    public String Name {get;set;}

    public String Description {get;set;}

    [Required]
    [Range(0,50)]
    public Double WalkLengthInKm {get; set;}

    public String? WalkImageUrl {get;set;}

    [Required]
    public Guid DifficultyId {get;set;}


    public Guid RegionId {get;set;}


};

}