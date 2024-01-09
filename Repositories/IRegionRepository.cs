using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    //Interface with which Regions Controller interacts, calling methods from SQLRegionRepository that have been injected
    //in Program.cs 
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
    }




}