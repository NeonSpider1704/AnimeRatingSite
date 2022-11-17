using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnimeRatingSite.Data;
using AnimeRatingSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace AnimeRatingSite.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class Animes1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Animes1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Animes1
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Anime.Include(a => a.Genre);
            return View(await applicationDbContext.OrderBy(a=>a.Title).ToListAsync());
        }

        // GET: Animes1/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Anime == null)
            {
                return NotFound();
            }

            var anime = await _context.Anime
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.AnimeId == id);
            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        // GET: Animes1/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genre.OrderBy(g=>g.Name), "GenreId", "Name");
            return View();
        }

        // POST: Animes1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimeId,Title,Rating,Description,GenreId")] Anime anime, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                if(Image != null)
                {
                    var fileName = UploadImage(Image);
                    anime.Image = fileName;
                }
                _context.Add(anime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "Name", anime.GenreId);
            return View(anime);
        }

        // GET: Animes1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Anime == null)
            {
                return NotFound();
            }

            var anime = await _context.Anime.FindAsync(id);
            if (anime == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "Name", anime.GenreId);
            return View(anime);
        }

        // POST: Animes1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimeId,Title,Rating,Description,GenreId")] Anime anime, IFormFile? Image, string? CurrentImage)
        {
            if (id != anime.AnimeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(Image != null)
                    {
                        var fileName = UploadImage(Image);
                        anime.Image = fileName;
                    }
                    else
                    {
                        anime.Image = CurrentImage;
                    }
                    _context.Update(anime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimeExists(anime.AnimeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "Name", anime.GenreId);
            return View(anime);
        }

        // GET: Animes1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Anime == null)
            {
                return NotFound();
            }

            var anime = await _context.Anime
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.AnimeId == id);
            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        // POST: Animes1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Anime == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Anime'  is null.");
            }
            var anime = await _context.Anime.FindAsync(id);
            if (anime != null)
            {
                _context.Anime.Remove(anime);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimeExists(int id)
        {
          return _context.Anime.Any(e => e.AnimeId == id);
        }

        private static string UploadImage(IFormFile Image)
        {
            var filePath = Path.GetTempFileName();
            var fileName = Guid.NewGuid() + "-" + Image.FileName;
            var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\img\\animes\\" + fileName;
            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                Image.CopyTo(stream);
            }
            return fileName;
        }
    }
}
