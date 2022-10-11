using Microsoft.AspNetCore.Mvc;
using AnimeRatingSite.Models;


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
            if(GenreName == null)
            {
                return RedirectToAction("Index");
            }

            ViewData["GenreName"] = GenreName;

            // Anime model to display place holders
            var animes = new List<Anime>();
            for(var i = 1; i < 11; i++)
            {
                animes.Add(new Anime { AnimeId = i, Title = "Anime" + i.ToString() });
            }

            return View(animes);
        }
    }
}
