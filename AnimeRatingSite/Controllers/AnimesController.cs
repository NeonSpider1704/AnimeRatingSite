using Microsoft.AspNetCore.Mvc;

namespace AnimeRatingSite.Controllers
{
    public class AnimesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Genre(string GenreName)
        {
            return View();
        }
    }
}
