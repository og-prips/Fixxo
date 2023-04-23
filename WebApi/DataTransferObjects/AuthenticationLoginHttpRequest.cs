namespace WebApi.DataTransferObjects;

public class AuthenticationLoginHttpRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
