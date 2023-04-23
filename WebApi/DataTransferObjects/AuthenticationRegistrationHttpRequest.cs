using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using WebApi.Models.Identity;

namespace WebApi.DataTransferObjects;

public class AuthenticationRegistrationHttpRequest
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    public string Email { get; set; } = null!;

    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$")]
    public string Password { get; set; } = null!;
    public string Role { get; set; } = "User";

    public static implicit operator CustomIdentityUser(AuthenticationRegistrationHttpRequest request)
    {
        return new CustomIdentityUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            
            UserName = request.Email,
            Email = request.Email,
        };
    }
}
