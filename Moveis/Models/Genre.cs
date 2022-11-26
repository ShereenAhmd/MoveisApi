using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moveis.Models
{
    public class Genre
    {
       
        public int id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
