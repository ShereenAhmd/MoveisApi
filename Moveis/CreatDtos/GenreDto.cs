using System.ComponentModel.DataAnnotations;
namespace Moveis.Controllers


{
    public class GenreDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
