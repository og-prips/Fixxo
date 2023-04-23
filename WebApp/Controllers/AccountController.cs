using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class AccountController : Controller
{
    private readonly IConfiguration _config;
    private readonly string _apiUrl;
    private readonly string _apiKey;

    public AccountController(IConfiguration config)
    {
        _config = config;
        _apiUrl = _config.GetSection("Api").GetValue<string>("Url")!;
        _apiKey = _config.GetSection("Api").GetValue<string>("Key")!;
    }

    public IActionResult Index()
    {
        if (RoleVerifyer.VerifyUserHasRole(new List<string> { "Admin", "ProductManager" }, HttpContext))
        {
            return RedirectToAction("Admin", "Account");
        }
        else if (RoleVerifyer.VerifyUserHasRole(new List<string> { "User" }, HttpContext))
        {
            return View();
        }

        return RedirectToAction("Login", "Auth");
    }

    public IActionResult Admin()
    {
        if (RoleVerifyer.VerifyUserHasRole(new List<string> { "Admin", "ProductManager" }, HttpContext))
        {
            var viewModel = new ProductViewModel();

            if (TempData.ContainsKey("IsSent"))
            {
                ViewBag.IsSent = TempData["IsSent"];
            }

            return View(viewModel);
        }

        return RedirectToAction("Login", "Auth");
    }

    [HttpPost]
    public async Task<IActionResult> Admin(ProductViewModel viewModel)
    {
        if (RoleVerifyer.VerifyUserHasRole(new List<string> { "Admin", "ProductManager" }, HttpContext))
        {
            if (ModelState.IsValid)
            {
                using var http = new HttpClient();
                var result = await http.PostAsJsonAsync($"{_apiUrl}/products?key={_apiKey}", viewModel);

                if (result.IsSuccessStatusCode)
                {
                    ModelState.Clear();
                    TempData["IsSent"] = true;
                    return RedirectToAction(nameof(Admin));
                }

                return BadRequest(result);
            }

            return View(viewModel);
        }

        return RedirectToAction("Login", "Auth");
    }

    public async Task<IActionResult> AdminProductsManager(int productId)
    {
        if (RoleVerifyer.VerifyUserHasRole(new List<string> { "Admin", "ProductManager" }, HttpContext))
        {
            using var http = new HttpClient();

            if (productId > 0)
            {
                await http.PostAsJsonAsync($"{_apiUrl}/products/delete/{productId}?key={_apiKey}", productId);
            }

            var productsResult = await http.GetFromJsonAsync<IEnumerable<ProductModel>>($"{_apiUrl}/products?key={_apiKey}");
            return View(productsResult);
        }

        return RedirectToAction("Login", "Auth");
    }
}
