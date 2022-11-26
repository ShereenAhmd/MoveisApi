//using Moveis.Models;
using System.ComponentModel.DataAnnotations;

namespace Moveis.CreatDtos
{
    public class MoveiDto
    {
        public String Title { get; set; }
        [MaxLength(2500)]
        public string StoreLine { get; set; }
        public double Rate { get; set; }
        public IFormFile? Poster { get; set; }
        public int Year { get; set; }
        public int GenreId { get; set; }
        
    }
}
