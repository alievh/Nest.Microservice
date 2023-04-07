using Nest.Web.Models;

namespace Nest.Web.Services.Interfaces;

public interface IVendorService
{
    Task<List<UserViewModel>> GetVendors();
}
