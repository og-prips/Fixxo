using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace _01_AspNetMVC.Controllers;

public class ContactController : Controller
{
    private readonly IConfiguration _config;

    public ContactController(IConfiguration config)
    {
        _config = config;
    }

    public IActionResult Index()
    {
        var viewModel = new ContactViewModel();

        if (TempData.ContainsKey("IsSent"))
        {
            ViewBag.IsSent = TempData["IsSent"];
        }

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(ContactViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            string apiUrl = _config.GetSection("Api").GetValue<string>("Url")!;
            string apiKey = _config.GetSection("Api").GetValue<string>("Key")!;

            using var http = new HttpClient();
            var result = await http.PostAsJsonAsync($"{apiUrl}/comment?key={apiKey}", viewModel);

            if (result.IsSuccessStatusCode)
            {
                ModelState.Clear();
                TempData["IsSent"] = true;
                return RedirectToAction(nameof(Index));
            }

            return BadRequest(result);
        }

        return View(viewModel);
    }
}
