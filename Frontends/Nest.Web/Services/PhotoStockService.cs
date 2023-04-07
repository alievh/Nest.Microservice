using Nest.Shared.DTO_s;
using Nest.Web.Models.PhotoStock;
using Nest.Web.Services.Interfaces;

namespace Nest.Web.Services;

public class PhotoStockService : IPhotoStockService
{
    private readonly HttpClient _httpClient;

    public PhotoStockService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PhotoStockViewModel> UploadPhoto(List<IFormFile> photos)
    {
        if (photos == null || photos.Count <= 0)
        {
            return null;
        }

        MultipartFormDataContent multipartContent = new MultipartFormDataContent();
        foreach (var photo in photos)
        {
            var randomFilename = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";

            using var ms = new MemoryStream();
            await photo.CopyToAsync(ms);

            multipartContent.Add(new ByteArrayContent(ms.ToArray()), "photos", randomFilename);
        }

        var response = await _httpClient.PostAsync("pictures", multipartContent);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSucces = await response.Content.ReadFromJsonAsync<ResponseDto<PhotoStockViewModel>>();

        return responseSucces.Data;
    }

    public async Task<bool> DeletePhoto(string photoUrl)
    {
        var response = await _httpClient.DeleteAsync($"photos?photoUrl={photoUrl}");
        return response.IsSuccessStatusCode;
    }

}
