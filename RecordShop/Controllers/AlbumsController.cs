using RecordShop.DataModels;
using RecordShop.Services;
using Microsoft.AspNetCore.Mvc;
namespace RecordShop.Controllers
{
    [ApiController]
    [Route("api/albums")]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumsService _albumsService;
        public AlbumsController(IAlbumsService albumsService)
        {
            _albumsService = albumsService;
        }

        [HttpGet]
        public IActionResult GetAllAlbums()
        {
            var albums = _albumsService.GetAllAlbums();
            return Ok(albums);
        }

        [HttpGet("{albumId}")]
        public IActionResult GetAlbumById(int albumId)
        {
            var album = _albumsService.GetAlbumById(albumId);
            return album is not null ? Ok(album) : NotFound();
        }

        [HttpPost]
        public IActionResult PostNewAlbum(Album newAlbum)
        {
            if (newAlbum == null)
            {
                return BadRequest("Album cannot be null");
            }
            else if (string.IsNullOrWhiteSpace(newAlbum.Title) || string.IsNullOrWhiteSpace(newAlbum.Artist) || string.IsNullOrWhiteSpace(newAlbum.Genre))
            {
                return BadRequest("Invalid album info");
            }
            var createdAlbum = _albumsService.AddNewAlbum(newAlbum);
            return CreatedAtAction(nameof(GetAlbumById), new { albumId = createdAlbum.AlbumId }, createdAlbum);
        }

        [HttpPut("{albumId}")]
        public IActionResult UpdateAlbum(int albumId, [FromBody] Album updatedAlbum)
        {
            if (updatedAlbum == null)
            {
                return BadRequest("Album cannot be null");
            }
            else if (string.IsNullOrWhiteSpace(updatedAlbum.Title) || string.IsNullOrWhiteSpace(updatedAlbum.Artist) || string.IsNullOrWhiteSpace(updatedAlbum.Genre))
            {
                return BadRequest("Invalid album info");
            }

            var albumAfterUpdate = _albumsService.UpdateAlbum(albumId, updatedAlbum);

            if (albumAfterUpdate == null)
            {
                return NotFound("AlbumId not found");
            }
            return Ok(albumAfterUpdate);
        }
        [HttpDelete("{albumId}")]
        public IActionResult DeleteAlbumById(int albumId)
        {
            var deleted = _albumsService.DeleteAlbumById(albumId);
            return deleted ? NoContent() : NotFound("Album not found");
        }
        [HttpGet("artist/{artistName}")]
        public IActionResult GetAllAlbumsByArtist(string artistName) 
        { 
            var albumsByArtist = _albumsService.GetAllAlbumsByArtist(artistName);
            return Ok(albumsByArtist);
        }
    }
}
