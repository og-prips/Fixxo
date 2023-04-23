using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace _01_AspNetMVC.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _config;

    public HomeController(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IActionResult> Index()
    {
        string apiUrl = _config.GetSection("Api").GetValue<string>("Url")!;
        string apiKey = _config.GetSection("Api").GetValue<string>("Key")!;

        using var http = new HttpClient();
        var result = await http.GetFromJsonAsync<IEnumerable<ProductModel>>($"{apiUrl}/products?key={apiKey}");

        return View(result);
    }
}
