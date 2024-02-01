using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ImagesController : ControllerBase
    {

            public ImagesController (NZWalksDbContext dbContext) 
            {
                this.dbContext = dbContext;
            }


            //POST: /api/Images/Upload
            [HttpPost]
            [Route("Upload")]
            public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
            {

                ValidateFileUpload(imageUploadRequestDto);

                if (ModelState.IsValid)
                {
                    //Convert DTO to Database Model  
                    var imageDatabaseModel = new Image
                    {
                        File = request.File,
                        FileExtension = Path.GetExtension(request.File.FileName),
                        FileSizeInBytes = request.File.Length,
                        FileName = request.FileName,
                        FileDescription = request.FileDescription,
                    };




                }

                
                return BadRequest(ModelState);

            }

            private void ValidateFileUpload(ImageUploadRequestDto request)
            {
                var allowedImageFileExtensions = new string[] {
                    ".jpeg",
                    ".jpg",
                    ".png"
                };

                if (!allowedImageFileExtensions.Contains(Path.GetExtension(request.File.FileName)))
                {
                    ModelState.AddModelError("file","Unsupported file extension (only .jpeg. .jpg, .png files accepted)");
                }

                var MaxFileSizeInBytes = 10485760;
                if (request.File.Length > MaxFileSizeInBytes)
                {
                    ModelState.AddModelError("file","File size more than 10MB: please upload a smaller file...");
                }

            }




    }


}