using Microsoft.AspNetCore.Mvc;
using Nest.Services.PictureStockApi.DTO_s;
using Nest.Shared.ControllerBases;
using Nest.Shared.DTO_s;

namespace Nest.Services.PictureStockApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PicturesController : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> PhotoSave(List<IFormFile> photos, CancellationToken cancellationToken)
    {
        if (photos != null && photos.Count > 0)
        {
            PictureDto photoDto = new();

            foreach (var photo in photos)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);

                var returnPath = photo.FileName;

                photoDto.Urls.Add(returnPath);
            }

            return CreateActionResultInstance(ResponseDto<PictureDto>.Success(photoDto, 200));
        }

        return CreateActionResultInstance(ResponseDto<PictureDto>.Fail("Picture is empty", 400));
    }

    public IActionResult PhotoDelete(string photoUrl)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);

        if (!System.IO.File.Exists(path))
        {
            return CreateActionResultInstance(ResponseDto<NoContent>.Fail("Photo not found!", 404));
        }

        System.IO.File.Delete(path);
        return CreateActionResultInstance(ResponseDto<NoContent>.Success(204));
    }
}
