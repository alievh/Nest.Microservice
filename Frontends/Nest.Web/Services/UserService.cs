using Nest.Web.Models;
using Nest.Web.Services.Interfaces;

namespace Nest.Web.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserViewModel> GetUser()
        => await _httpClient.GetFromJsonAsync<UserViewModel>("/api/user/getuser");

}
