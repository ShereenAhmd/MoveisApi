using Moveis.Models;
using System.ComponentModel.DataAnnotations;

namespace Moveis.CreatDtos
{
    public class MoveiDetailsDto
    {

        public int Id { get; set; }
        public String Title { get; set; }
        
        public string StoreLine { get; set; }
        public double Rate { get; set; }
        public byte[] Poster { get; set; }
        public int Year { get; set; }
        public int GenreId { get; set; }
        public String GenreName { get; set; }
    }
}
