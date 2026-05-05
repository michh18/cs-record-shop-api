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
            var createdAlbum = _albumsService.AddNewAlbum(newAlbum);
            return CreatedAtAction(nameof(GetAlbumById), new { albumId = createdAlbum.AlbumId }, createdAlbum);
        }
    }
}
