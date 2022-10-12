using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AnimeRatingSite.Models;

namespace AnimeRatingSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AnimeRatingSite.Models.Anime> Anime { get; set; }
        public DbSet<AnimeRatingSite.Models.Genre> Genre { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}