using Nest.Web.Models.PhotoStock;

namespace Nest.Web.Services.Interfaces;

public interface IPhotoStockService
{
    Task<PhotoStockViewModel> UploadPhoto(List<IFormFile> photo);
    Task<bool> DeletePhoto(string photoUrl);
}
