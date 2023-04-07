using System.ComponentModel.DataAnnotations;

namespace Nest.Web.Models.Authentication;

public class SignInInput
{
    [Required]
    [Display(Name = "Your email address")]
    public string Email { get; set; }
    [Required]
    [Display(Name = "Your password")]
    public string Password { get; set; }
    public bool IsRemember { get; set; }
}
