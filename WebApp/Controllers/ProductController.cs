using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> Index(int productId)
        {
            using var client = new HttpClient();
            var result = await client.GetFromJsonAsync<ProductModel>($"https://localhost:7049/api/products/{productId}");

            return View(result);
        }
    }
}
