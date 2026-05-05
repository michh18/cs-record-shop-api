using RecordShop.DataModels;

namespace RecordShop.Repositories
{
    public interface IAlbumsRepository
    {
        public IEnumerable<Album> GetAllAlbums();
        //public Album GetAlbumById(int albumId);
        //public Album AddNewAlbum(Album album);
        //public Album UpdateAlbum(Album updatedAlbum);
        //public bool DeleteAlbum(int albumId);
        //public List<Album> GetAllAlbumsByArtist(string artistName);
        //public List<Album> GetAllAlbumsByReleaseYear(int releaseYear);
        //public List<Album> GetAllAlbumsByGenre(string genre);

    }
    public class AlbumsRepository : IAlbumsRepository
    {
        private readonly RecordShopDbContext _context;
        public AlbumsRepository(RecordShopDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Album> GetAllAlbums()
        {
            return _context.Albums.ToList();
        }
    }
}

