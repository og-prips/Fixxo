using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApi.DataTransferObjects;
using WebApi.Helpers;
using WebApi.Models.Identity;

namespace WebApi.Services;

public class AuthService
{
    private readonly SignInManager<CustomIdentityUser> _signInManager;
    private readonly UserManager<CustomIdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtToken _jwtToken;

    public AuthService(SignInManager<CustomIdentityUser> signInManager, UserManager<CustomIdentityUser> userManager, RoleManager<IdentityRole> roleManager, JwtToken jwtToken)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtToken = jwtToken;
    }

    public async Task<bool> RegisterAsync(AuthenticationRegistrationHttpRequest request)
    {
        if (!await _roleManager.Roles.AnyAsync())
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("ProductManager"));
            await _roleManager.CreateAsync(new IdentityRole("User"));
        }

        int userCount = await _userManager.Users.CountAsync();

        request.Role = userCount == 0 ? "Admin" : userCount == 1 ? "ProductManager" : "User";

        CustomIdentityUser user = request;

        var createResult = await _userManager.CreateAsync(user, request.Password);

        if (createResult.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, request.Role);

            var isSignedIn = await _signInManager.PasswordSignInAsync(user.Email!, request.Password, false, false);
            if (isSignedIn.Succeeded)
            {
                return true;
            }
        }

        return false;
    }

    public async Task<string> LoginAsync(AuthenticationLoginHttpRequest request)
    {
        var identityUser = await _userManager.FindByEmailAsync(request.Email);
        if (identityUser != null)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
            if (signInResult.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(identityUser);

                var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", identityUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, identityUser.Email!),
                    new Claim(ClaimTypes.Role, roles[0])
                });

                return _jwtToken.Generate(claimsIdentity, DateTime.Now.AddHours(1));
            }
        }

        return string.Empty;
    }
}
