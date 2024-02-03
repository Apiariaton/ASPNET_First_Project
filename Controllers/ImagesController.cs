using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ImagesController : ControllerBase
    {

            private readonly IImageRepository imageRepository;
            public ImagesController (IImageRepository imageRepository) 
            {
                this.imageRepository = imageRepository;
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
                        File = imageUploadRequestDto.File,
                        FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                        FileSizeInBytes = imageUploadRequestDto.File.Length,
                        FileName = imageUploadRequestDto.FileName,
                        FileDescription = imageUploadRequestDto.FileDescription,
                    };

                    await imageRepository.Upload(imageDatabaseModel);

                    return Ok(imageDatabaseModel);

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