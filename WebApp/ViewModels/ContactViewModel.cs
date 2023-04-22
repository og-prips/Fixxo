using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class ContactViewModel
{
	[Required(ErrorMessage = "Please enter a name")]
	[MinLength(2, ErrorMessage = "Name has to be atleast 2 characters")]
    public string CustomerName { get; set; } = null!;

	[EmailAddress]
	[Required(ErrorMessage = "Please enter an email")]
	public string Email { get; set; } = null!;

	[Required(ErrorMessage = "Please enter a comment")]
	public string Comment { get; set; } = null!;

	public bool IsSent { get; set; } = false;
}
