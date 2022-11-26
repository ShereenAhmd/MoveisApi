using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moveis.Models;


namespace Moveis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genre = await _context.Genres.OrderBy(m => m.Name).ToListAsync();
            return Ok(genre);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();
            return Ok(genre);
        }

        [HttpPut(template: "{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] GenreDto dto)
        {
            
            var genre = await _context.Genres.SingleOrDefaultAsync(m => m.id == id);
            if (genre == null)
                return NotFound($"No Genre Found With ID:{id}");
            genre.Name = dto.Name;
            _context.SaveChanges();
            return Ok(genre);

        }

        [HttpDelete(template: "{id}")]
        public async Task<IActionResult> DeletAsync(int id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g=>g.id == id);
            if(genre == null)
            return NotFound($"No Genre was Found With ID:{id}");
            _context.Remove(genre);
             
            _context.SaveChanges();
            return Ok(genre );
        }
        

    }
}
