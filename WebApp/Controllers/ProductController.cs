using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration _config;

        public ProductController(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IActionResult> Index(int productId)
        {
            string apiUrl = _config.GetSection("Api").GetValue<string>("Url")!;
            string apiKey = _config.GetSection("Api").GetValue<string>("Key")!;

            using var http = new HttpClient();
            var result = await http.GetFromJsonAsync<ProductModel>($"{apiUrl}/products/{productId}?key={apiKey}");

            return View(result);
        }
    }
}
