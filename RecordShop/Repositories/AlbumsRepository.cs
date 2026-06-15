using RecordShop.DataModels;

namespace RecordShop.Repositories
{
    public interface IAlbumsRepository
    {
        public IEnumerable<Album> GetAllAlbums();
        public Album GetAlbumById(int albumId);
        public Album AddNewAlbum(Album album);
        public Album UpdateAlbum(int albumId, Album updatedAlbum);
        public bool DeleteAlbumById(int albumId);
        public List<Album> GetAllAlbumsByArtist(string artistName);
        public List<Album> GetAllAlbumsByReleaseYear(int releaseYear);
        public List<Album> GetAllAlbumsByGenre(string genre);

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
        public Album GetAlbumById(int albumId)
        {
            return _context.Albums.FirstOrDefault(a => a.AlbumId == albumId);
        }
        public Album AddNewAlbum(Album newAlbum) 
        {
            _context.Albums.Add(newAlbum);
            _context.SaveChanges();
            return newAlbum;
        }
        public Album UpdateAlbum(int albumId, Album updatedAlbum) 
        {
            var existingAlbum = GetAlbumById(albumId);
            if (existingAlbum == null) 
            {
                return null;
            }
            existingAlbum.Title = updatedAlbum.Title;
            existingAlbum.Artist = updatedAlbum.Artist;
            existingAlbum.Genre = updatedAlbum.Genre;
            existingAlbum.ReleaseYear = updatedAlbum.ReleaseYear;
            existingAlbum.Price = updatedAlbum.Price;
            existingAlbum.StockQuantity = updatedAlbum.StockQuantity;

            _context.SaveChanges();
            return existingAlbum;
        }
        public bool DeleteAlbumById(int albumId) 
        {
            var existingAlbum = GetAlbumById(albumId);
            if (existingAlbum == null)
            {
                return false;
            }
            _context.Albums.Remove(existingAlbum);
            _context.SaveChanges();
            return true;
        }
        public List<Album> GetAllAlbumsByArtist(string artistName) 
        {
            return _context.Albums.Where(a => a.Artist.Equals(artistName, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public List<Album> GetAllAlbumsByReleaseYear(int releaseYear) 
        {
            return _context.Albums.Where(a => a.ReleaseYear == releaseYear).ToList();
        }
        public List<Album> GetAllAlbumsByGenre(string genre)
        {
            return _context.Albums.Where(a => a.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}

