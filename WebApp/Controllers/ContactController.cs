using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace _01_AspNetMVC.Controllers;

public class ContactController : Controller
{
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
            using var http = new HttpClient();
            var result = await http.PostAsJsonAsync("https://localhost:7049/api/comment", viewModel);

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
