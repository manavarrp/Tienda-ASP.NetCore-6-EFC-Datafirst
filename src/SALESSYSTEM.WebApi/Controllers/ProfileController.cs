using Microsoft.AspNetCore.Mvc;

namespace SALESSYSTEM.WebApi.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
