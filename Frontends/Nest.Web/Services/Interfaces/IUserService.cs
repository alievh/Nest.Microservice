using Nest.Web.Models;

namespace Nest.Web.Services.Interfaces;

public interface IUserService
{
    Task<UserViewModel> GetUser();
}
