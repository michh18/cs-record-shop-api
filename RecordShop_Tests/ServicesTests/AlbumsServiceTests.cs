using Moq;
using RecordShop.DataModels;
using RecordShop.Repositories;
using RecordShop.Services;

namespace RecordShop_Tests.ServiceTests
{
    public class AlbumsServiceTests
    {
        private Mock<IAlbumsRepository> _albumRepositoryMock;
        private AlbumsService _albumService;
        private IEnumerable<Album> _albums;

        [SetUp]
        public void Setup()
        {
            _albumRepositoryMock = new Mock<IAlbumsRepository>();
            _albumService = new AlbumsService(_albumRepositoryMock.Object);
            _albums = new List<Album>
            {
                new Album {AlbumId = 1, Title = "Thriller", Artist = "Michael Jackson", Genre = "Pop", ReleaseYear = 1982, Price = 11.99m, StockQuantity = 8 },
                new Album {AlbumId = 2, Title = "21", Artist = "Adele", Genre = "Pop", ReleaseYear = 2011, Price = 9.99m, StockQuantity = 10},
                new Album {AlbumId = 3, Title = "Back to Black", Artist = "Amy Winehouse", Genre = "Soul", ReleaseYear = 2006, Price = 10.99m, StockQuantity = 3},
                new Album {AlbumId = 4, Title = "Abbey Road", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1969, Price = 12.99m, StockQuantity = 5},
                new Album {AlbumId = 5, Title = "To Pimp a Butterfly", Artist = "Kendrick Lamar", Genre = "Hip-Hop", ReleaseYear = 2015, Price = 13.49m, StockQuantity = 4}
            };
        }

        [Test]
        public void GetAllAlbums_ReturnsAllAlbums()
        {
            _albumRepositoryMock.Setup(repo => repo.GetAllAlbums()).Returns(_albums);
            var result = _albumService.GetAllAlbums();
            Assert.That(result, Is.EqualTo(_albums));
        }

        [Test]
        public void GetAllAlbums_ReturnsEmptyList_WhenNoAlbumsExist()
        {
            _albumRepositoryMock.Setup(repo => repo.GetAllAlbums()).Returns(new List<Album>());
            var result = _albumService.GetAllAlbums();
            Assert.IsEmpty(result);
        }
    }
}