using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{


    public interface IWalksRepository
    {

        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk> CreateAsync(Walk walk);









    }






}