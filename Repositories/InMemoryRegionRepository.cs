using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories{

public class InMemoryRegionRepository : IRegionRepository
{

    public async Task<List<Region>> GetAllAsync()
    {

        var dummyRegionList = new List<Region>
        {
            new Region()
        {
            Id = Guid.NewGuid(),
            Code = "NEW",
            Name = "New Mal",
            RegionImageUrl = "new_image.jpeg"
        }
        };

        return dummyRegionList;
    }




}





};