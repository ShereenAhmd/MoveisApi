using System.ComponentModel.DataAnnotations;

namespace Moveis.Models
{
    public class Movei
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public String Title { get; set; }
        [MaxLength(2500)]
        public string StoreLine { get; set; }
        public double Rate { get; set; }
        public byte[] Poster { get; set; }
        public int Year { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
