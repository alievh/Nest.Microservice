using Nest.Web.Models;
using Nest.Web.Services.Interfaces;

namespace Nest.Web.Services;

public class VendorService : IVendorService
{
    private readonly HttpClient _httpClient;

    public VendorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserViewModel>> GetVendors()
        => await _httpClient.GetFromJsonAsync<List<UserViewModel>>("/api/user/getVendors");
}
