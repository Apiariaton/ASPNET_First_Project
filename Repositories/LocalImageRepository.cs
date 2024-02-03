using NZWalks.API.Models.Domain;
using static Microsoft.AspNetCore.Hosting.IWebHostEnvironment;
using NZWalks.API.Data;

namespace NZWalks.API.Repositories
{

    public class LocalImageRepository : IImageRepository
    {

        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly NZWalksDbContext dbContext;
        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
        IHttpContextAccessor httpContextAccessor,
        NZWalksDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }


        public async Task<Image> Upload(Image image)
        {
            var localFilePathToImagesFolder = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            //Upload image file to Images Folder
            using var stream = new FileStream(localFilePathToImagesFolder,FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }


    }


}