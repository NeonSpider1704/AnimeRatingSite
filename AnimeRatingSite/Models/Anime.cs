using System.ComponentModel.DataAnnotations;

namespace AnimeRatingSite.Models
{
    public class Anime
    {
        public int AnimeId { get; set; }

        [Required]
        [MaxLength(150)]
        public string? Title { get; set; }
        [Range(0,10, ErrorMessage = "Rating must be between 0 and 10")]
        public double Rating { get; set; }
        [MaxLength(4000)]
        public string? Description { get; set; }

        public string? Image { get; set; }

        public int GenreId { get; set; }

        //Paretn reference for auto-joins
        public Genre? Genre { get; set; }
    }
}
