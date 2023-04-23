using Microsoft.AspNetCore.Identity;

namespace WebApi.Models.Identity;

public class CustomIdentityUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
