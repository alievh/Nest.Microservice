using Nest.Web.Models;
using Nest.Web.Models.Order;

namespace Nest.Web.ViewModels;

public class AccountVM
{
    public UserViewModel User { get; set; }
    public List<OrderViewModel> Orders { get; set; }
}
