using System.ComponentModel.DataAnnotations;
namespace WebApp.ViewModels;

public class LoginViewModel
{
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Please enter an email address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Display(Name = "Password")]
    [Required(ErrorMessage = "Please enter a password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
