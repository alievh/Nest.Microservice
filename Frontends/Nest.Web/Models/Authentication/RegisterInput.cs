using System.ComponentModel.DataAnnotations;

namespace Nest.Web.Models.Authentication;

public class RegisterInput
{
    [Required]
    [Display(Name = "Your username")]
    public string Username { get; set; }
    [Required]
    [Display(Name = "Your email address")]
    public string Email { get; set; }
    [Required]
    [Display(Name = "Your password")]
    public string Password { get; set; }

    [Required]
    public string Role { get; set; }
    //[Display(Name = "Confirm Password")]
    //public string ConfirmPassword { get; set; }
}
