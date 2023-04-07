using Microsoft.Extensions.Options;
using Nest.Web.Models;

namespace Nest.Web.Helpers;

public class PhotoHelper
{
    private readonly ServiceApiSettings _serviceApiSettings;

    public PhotoHelper(IOptions<ServiceApiSettings> serviceApiSettings)
    {
        _serviceApiSettings = serviceApiSettings.Value;
    }

    public List<string> GetPhotoStockUrl(List<string> photoUrls)
    {
        List<string> photoUrlsList = new List<string>();
        foreach (var photoUrl in photoUrls)
        {
            photoUrlsList.Add($"{_serviceApiSettings.PhotoStockUri}/photos/{photoUrl}");
        }
        return photoUrlsList;
    }
}
