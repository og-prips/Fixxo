using Microsoft.AspNetCore.Mvc;

namespace _01_AspNetMVC.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
