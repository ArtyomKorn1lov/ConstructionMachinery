using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AdvertController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
