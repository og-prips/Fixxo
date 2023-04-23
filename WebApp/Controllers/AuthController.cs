using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class AuthController : Controller
{
    private readonly IConfiguration _config;
    private readonly string _apiUrl;
    private readonly string _apiKey;

    public AuthController(IConfiguration config)
    {
        _config = config;
        _apiUrl = _config.GetSection("Api").GetValue<string>("Url")!;
        _apiKey = _config.GetSection("Api").GetValue<string>("Key")!;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            using var http = new HttpClient();
            var result = await http.PostAsJsonAsync($"{_apiUrl}/Auth/Register?key={_apiKey}", viewModel);

            if (result.IsSuccessStatusCode)
            {
                var loginResult = await http.PostAsJsonAsync($"{_apiUrl}/Auth/Login?key={_apiKey}", new LoginViewModel()
                {
                    Email = viewModel.Email,
                    Password = viewModel.Password,
                });

                if (loginResult.IsSuccessStatusCode)
                {
                    var token = await loginResult.Content.ReadAsStringAsync();

                    HttpContext.Response.Cookies.Append("accessToken", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.Now.AddHours(1)
                    });

                    return RedirectToAction("Index", "Account");
                }

                HttpContext.Response.Cookies.Delete("accessToken");
                return RedirectToAction("Login", "Auth");
            }
        }

        return View(viewModel);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            using var http = new HttpClient();
            var result = await http.PostAsJsonAsync($"{_apiUrl}/Auth/Login?key={_apiKey}", viewModel);

            if (result.IsSuccessStatusCode)
            {
                var token = await result.Content.ReadAsStringAsync();

                HttpContext.Response.Cookies.Append("accessToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.Now.AddHours(1)
                });

                return RedirectToAction("Index", "Account");
            }

            var responseContent = await result.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, responseContent);
        }

        return View(viewModel);
    }
}
