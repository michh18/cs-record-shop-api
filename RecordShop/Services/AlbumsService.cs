using Microsoft.EntityFrameworkCore.Query;
using RecordShop.DataModels;
using RecordShop.Repositories;

namespace RecordShop.Services
{
    public interface IAlbumsService
    {
        public IEnumerable<Album> GetAllAlbums();
        public Album GetAlbumById(int albumId);
        public Album AddNewAlbum(Album newAlbum);
        public Album UpdateAlbum(int albumId, Album updatedAlbum);
        public bool DeleteAlbumById(int albumId);
        public List<Album> GetAllAlbumsByArtist(string artistName);

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
        public Album AddNewAlbum(Album newAlbum) 
        {
            return _albumsRepository.AddNewAlbum(newAlbum);
        }
        public Album UpdateAlbum(int albumId, Album updatedAlbum) 
        {
            return _albumsRepository.UpdateAlbum(albumId, updatedAlbum);
        }
        public bool DeleteAlbumById(int albumId) 
        {
            return _albumsRepository.DeleteAlbumById(albumId);
        }
        public List<Album> GetAllAlbumsByArtist(string artistName) 
        {
            return _albumsRepository.GetAllAlbumsByArtist(artistName);
        }
    }
}
