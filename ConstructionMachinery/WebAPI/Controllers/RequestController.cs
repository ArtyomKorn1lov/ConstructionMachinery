using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class RequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
