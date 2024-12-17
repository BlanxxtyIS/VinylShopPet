using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VinylShop.Api.Persistence;
using VinylShop.Api.Persistence.Entities;
using VinylShop.Shared.Features;

namespace VinylShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly VinylShopContext _context;

        public AlbumController(VinylShopContext context)
        {
            _context = context;
        }

        // GET: api/Album
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> GetAlbums()
        {
            var albums = await _context.Albums
                .Include(a => a.Song)
                .AsNoTracking()
                .Select(a => new AlbumDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Image = a.Image,
                    AuthorName = a.AuthorName,
                    TimeInMinutes = a.TimeInMinutes,
                    Rating = a.Rating,
                    Songs = a.Song.Select(s => new AlbumDTO.Song
                    {
                        Id = s.Id,
                        AlbumId = s.AlbumId,
                        Name = s.Name,
                        Text = s.Text
                    }).ToList()
                })
                .ToListAsync();

            return Ok(albums);
        }

        // GET: api/Album/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumDTO>> GetAlbum(int id)
        {
            var album = await _context.Albums
                .Include(a => a.Song)
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new AlbumDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Image = a.Image,
                    AuthorName = a.AuthorName,
                    TimeInMinutes = a.TimeInMinutes,
                    Rating = a.Rating,
                    Songs = a.Song.Select(s => new AlbumDTO.Song
                    {
                        Id = s.Id,
                        AlbumId = s.AlbumId,
                        Name = s.Name,
                        Text = s.Text
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

        // POST: api/Album
        [HttpPost]
        public async Task<ActionResult<AlbumDTO>> CreateAlbum(AlbumDTO albumDto)
        {
            var album = new Album
            {
                Name = albumDto.Name,
                Description = albumDto.Description,
                Image = albumDto.Image,
                AuthorName = albumDto.AuthorName,
                TimeInMinutes = albumDto.TimeInMinutes,
                Rating = albumDto.Rating,
                Song = albumDto.Songs.Select(s => new Song
                {
                    Name = s.Name,
                    Text = s.Text
                }).ToList()
            };

            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            albumDto.Id = album.Id;
            return CreatedAtAction(nameof(GetAlbum), new { id = album.Id }, albumDto);
        }

        // PUT: api/Album/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlbum(int id, AlbumDTO albumDto)
        {
            if (id != albumDto.Id)
            {
                return BadRequest();
            }

            var album = await _context.Albums
                .Include(a => a.Song)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (album == null)
            {
                return NotFound();
            }

            album.Name = albumDto.Name;
            album.Description = albumDto.Description;
            album.Image = albumDto.Image;
            album.AuthorName = albumDto.AuthorName;
            album.TimeInMinutes = albumDto.TimeInMinutes;
            album.Rating = albumDto.Rating;

            // Обновляем песни
            _context.Songs.RemoveRange(album.Song);
            album.Song = albumDto.Songs.Select(s => new Song
            {
                Name = s.Name,
                Text = s.Text,
                AlbumId = album.Id
            }).ToList();

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Album/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.Id == id);
        }
    }
}
