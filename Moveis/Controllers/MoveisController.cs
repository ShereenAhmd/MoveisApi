using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moveis.CreatDtos;
using Moveis.Models;

using System.Security.Cryptography.Xml;

namespace Moveis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoveisController : ControllerBase
    {
        private new List<String> _alloeExtension = new List<String> { ".jpg", ".png" };
        private long _MaximumAlowedPosterSize = 1048576;

        private readonly ApplicationDbContext _context;
        public MoveisController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> CreatAsync([FromForm] MoveiDto dto)
        {
            if (dto.Poster == null) return BadRequest("Poster is required");
            if (!_alloeExtension.Contains(Path.GetExtension(dto.Poster.FileName)))
            {
                return BadRequest(error: "Invalid Extension Only .jpg and .png Allowed ");
            }

            var IsValidGenreId = await _context.Genres.AnyAsync(g => g.id == dto.GenreId);
            if (!IsValidGenreId)
            {
                return BadRequest(error: "Invalid GenreId");
            }
            if (dto.Poster.Length > _MaximumAlowedPosterSize)
            { return BadRequest(error: "Max Allowed Size For Poster Is 1MB!"); }
            using var datastream = new MemoryStream();
            await dto.Poster.CopyToAsync(datastream);


            var movei = new Movei
            {
                GenreId = dto.GenreId,
                Title = dto.Title,
                Rate = dto.Rate,
                Year = dto.Year,
                StoreLine = dto.StoreLine,
                Poster = datastream.ToArray()

            };
            await _context.Movies.AddAsync(movei);
            _context.SaveChanges();
            return Ok(movei);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movei = await _context.Movies.Include(m=>m.Genre).Select(m=> new MoveiDetailsDto {
                Title = m.Title,
                Rate = m.Rate,
                Year = m.Year,
                GenreId = m.GenreId,
                GenreName = m.Genre.Name,
                StoreLine = m.StoreLine,
                Poster = m.Poster,
            }).ToListAsync();
             
            
            return Ok(movei);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsyncById(int id)
        {
            var movei = await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(i => i.Id == id);
            if (movei == null) { return NotFound(); }
            var dto = new MoveiDetailsDto
            {
                Id=movei.Id,
                Title = movei.Title,
                Rate = movei.Rate,
                Year = movei.Year,
                GenreId = movei.GenreId,
                GenreName = movei.Genre.Name,
                StoreLine = movei.StoreLine,
                Poster = movei.Poster,
            };
            return Ok(dto);
        }

        [HttpGet(template: "{GetByGenreId}")]
        public async Task<IActionResult> GetAsyncByGenreId(int GenreId)
        {
           
            var mov = await _context.Movies
                .Where(m => m.GenreId == GenreId)
                .Include(m => m.Genre)
                .Select(g => new MoveiDetailsDto
            {
                Id = g.Id,
                Title = g.Title,
                Rate = g.Rate,
                Year = g.Year,
                GenreId = g.GenreId,
                GenreName = g.Genre.Name,
                StoreLine = g.StoreLine,
                Poster = g.Poster,
            }).ToListAsync();


            return Ok(mov);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movei = await _context.Movies.FindAsync(id);
            if (movei == null) { return BadRequest(error: $"Not Found Id:{id}"); }
            _context.Remove(movei);
            _context.SaveChanges();
            return Ok(movei);
        }

        
        
        [HttpPut(template:"{id}")]
        public async Task<IActionResult> UpdateAsync(int id,[FromForm] MoveiDto dto)
        {
            var movei = await _context.Movies.FindAsync(id);
            if (movei == null) { return NotFound(value: $"Not Found Id:{id}"); }

            var IsValidGenreId = await _context.Genres.AnyAsync(g => g.id == dto.GenreId);
            if (!IsValidGenreId)
            {
                return BadRequest(error: "Invalid GenreId");
            }

            if (dto.Poster != null)
            {
                if (!_alloeExtension.Contains(Path.GetExtension(dto.Poster.FileName).ToLower())) { return BadRequest(error: $"Only .jpg and .png images are allowed "); }
                if (dto.Poster.Length > _MaximumAlowedPosterSize) return BadRequest(error: $"The Maximum Size Allowed Is 1MB!");

                var datastream = new MemoryStream();
                await dto.Poster.CopyToAsync(datastream);
                movei.Poster = datastream.ToArray();

            }
                

                movei.Title = dto.Title;
                movei.StoreLine = dto.StoreLine;
                movei.Rate = dto.Rate;
                movei.Year = dto.Year;
                movei.GenreId = dto.GenreId;
                
                           
                _context.SaveChanges();
                return Ok(movei);
            
            }
            
            


        
    }
}
    

