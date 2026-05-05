using RecordShop.DataModels;
using RecordShop.Repositories;

namespace RecordShop.Services
{
    public interface IAlbumsService
    {
        public IEnumerable<Album> GetAllAlbums();
        public Album GetAlbumById(int albumId);
    }

    public class AlbumsService : IAlbumsService
    {
        private readonly IAlbumsRepository _albumsRepository;
        public AlbumsService(IAlbumsRepository albumsRepository)
        {
            _albumsRepository = albumsRepository;
        }
        public IEnumerable<Album> GetAllAlbums()
        {
            return _albumsRepository.GetAllAlbums();
        }
        public Album GetAlbumById(int albumId) 
        { 
            return _albumsRepository.GetAlbumById(albumId);
        }
    }
}
