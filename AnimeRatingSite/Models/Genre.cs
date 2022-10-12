using System.ComponentModel.DataAnnotations;

namespace AnimeRatingSite.Models
{
    public class Genre
    {
        public int GenreId { get; set; }

        [Required]
        [MaxLength(150)]
        public string? Name { get; set; }

        public List<Anime>? Animes { get; set; }
    }
}