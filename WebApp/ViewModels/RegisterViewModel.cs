using System.ComponentModel.DataAnnotations;
namespace WebApp.ViewModels;

public class RegisterViewModel
{
    [Display(Name = "First name")]
    [Required(ErrorMessage = "Please enter an email address")]
    [MinLength(2, ErrorMessage = "First name must be over 2 characters")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last name")]
    [Required(ErrorMessage = "Please enter an email address")]
    [MinLength(2, ErrorMessage = "Last name must be over 2 characters")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Please enter an email address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Display(Name = "Password")]
    [Required(ErrorMessage = "Please enter a password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm password")]
    [Required(ErrorMessage = "Please confirm password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = null!;
}
