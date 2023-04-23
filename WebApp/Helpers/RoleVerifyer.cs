using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace WebApp.Helpers;

public static class RoleVerifyer
{
    public static bool VerifyUserHasRole(List<string> roles, HttpContext httpContext)
    {
        using var http = new HttpClient();
        var token = httpContext.Request.Cookies["accessToken"];
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var handler = new JwtSecurityTokenHandler();
        var readableToken = handler.ReadJwtToken(token);
        var claimRoles = readableToken.Claims.Where(x => x.Type == "role").Select(x => x.Value).ToList();

        foreach (string role in claimRoles)
        {
            if (roles.Contains(role))
            {
                return true;
            }
        }

        return false;
    }
}
