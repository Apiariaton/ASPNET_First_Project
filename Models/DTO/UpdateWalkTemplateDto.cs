using NZWalks.API.Models;




namespace NZWalks.API.Models.DTO
{
public class UpdateWalkTemplateDto{
    
    public String Name {get;set;}

    public String Description {get;set;}

    public Double WalkLengthInKm {get; set;}

    public String? WalkImageUrl {get;set;}

    public Guid DifficultyId {get;set;}


    public Guid RegionId {get;set;}


};

}