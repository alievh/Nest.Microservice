using Nest.Shared.DTO_s;
using Nest.Web.Helpers;
using Nest.Web.Models.Catalog;
using Nest.Web.Services.Interfaces;

namespace Nest.Web.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;
    private readonly IPhotoStockService _photoStockService;
    private readonly PhotoHelper _photoHelper;

    public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
    {
        _httpClient = httpClient;
        _photoStockService = photoStockService;
        _photoHelper = photoHelper;
    }
    public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
    {
        var response = await _httpClient.GetAsync("categories");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<List<CategoryViewModel>>>();

        return responseSuccess.Data;
    }


    public async Task<List<SubCategoryViewModel>> GetAllSubCategoryAsync()
    {
        var response = await _httpClient.GetAsync("subCategories");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<List<SubCategoryViewModel>>>();

        return responseSuccess.Data;
    }

    public async Task<List<ProductViewModel>> GetAllProductAsync()
    {
        var response = await _httpClient.GetAsync("products");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<List<ProductViewModel>>>();

        responseSuccess.Data.ForEach(x =>
        {
            x.StockPictureUrls = _photoHelper.GetPhotoStockUrl(x.Pictures);
        });

        return responseSuccess.Data;
    }
    public async Task<List<ProductViewModel>> GetAllProductByUserIdAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"products/getAllByUserId/{userId}");


        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<List<ProductViewModel>>>();

        responseSuccess.Data.ForEach(x =>
        {
            x.StockPictureUrls = _photoHelper.GetPhotoStockUrl(x.Pictures);
        });

        return responseSuccess.Data;
    }

    public async Task<List<ProductViewModel>> GetAllProductBySubCategoryAsync(string subCategoryId)
    {
        var response = await _httpClient.GetAsync($"products/getAllBySubCategory/{subCategoryId}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<List<ProductViewModel>>>();

        responseSuccess.Data.ForEach(x =>
        {
            x.StockPictureUrls = _photoHelper.GetPhotoStockUrl(x.Pictures);
        });

        return responseSuccess.Data;
    }

    public async Task<ProductViewModel> GetProductByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"products/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<ProductViewModel>>();

        responseSuccess.Data.StockPictureUrls = _photoHelper.GetPhotoStockUrl(responseSuccess.Data.Pictures);

        return responseSuccess.Data;
    }
    public async Task<bool> CreateProductAsync(ProductCreateInput productCreateInput)
    {
        var resultPhotoService = await _photoStockService.UploadPhoto(productCreateInput.PhotoFormFiles);

        if (resultPhotoService != null)
        {
            productCreateInput.Pictures = resultPhotoService.Urls;
        }

        var response = await _httpClient.PostAsJsonAsync("products", productCreateInput);

        return response.IsSuccessStatusCode;
    }
    public async Task<bool> UpdateProductAsync(ProductUpdateInput productUpdateInput)
    {
        var resultPhotoService = await _photoStockService.UploadPhoto(productUpdateInput.PhotoFormFiles);

        if (resultPhotoService != null)
        {
            foreach (var picture in productUpdateInput.Pictures)
            {
                await _photoStockService.DeletePhoto(picture);
            }
            productUpdateInput.Pictures = resultPhotoService.Urls;
        }
        else
        {
            productUpdateInput.Pictures = GetProductByIdAsync(productUpdateInput.Id).Result.Pictures;
        }

        var response = await _httpClient.PutAsJsonAsync("products", productUpdateInput);

        return response.IsSuccessStatusCode;
    }
    public async Task<bool> DeleteProductAsync(string productId)
    {
        var response = await _httpClient.DeleteAsync($"products/{productId}");

        return response.IsSuccessStatusCode;
    }

}
