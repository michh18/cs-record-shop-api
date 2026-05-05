using Moq;
using RecordShop.Controllers;
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

        // -------------------------- GetAllAlbums Tests --------------------------------
        [Test]
        public void GetAllAlbums_ReturnsAllAlbums()
        {
            _albumRepositoryMock.Setup(r => r.GetAllAlbums()).Returns(_albums);

            var result = _albumService.GetAllAlbums();

            Assert.That(result, Is.EqualTo(_albums));
        }
        [Test]
        public void GetAllAlbums_ReturnsEmptyList_WhenNoAlbumsExist()
        {
            _albumRepositoryMock.Setup(r => r.GetAllAlbums()).Returns(new List<Album>());

            var result = _albumService.GetAllAlbums();

            Assert.IsEmpty(result);
        }
        [Test]
        public void GetAllAlbums_ShouldInvokeGetAllAlbumsFromRepositoryLayer()
        {
            _albumRepositoryMock.Setup(r => r.GetAllAlbums()).Returns(_albums);

            _albumService.GetAllAlbums();

            _albumRepositoryMock.Verify(r => r.GetAllAlbums(), Times.Once);
        }

        // -------------------------- GetAlbumById Tests --------------------------------
        [Test]
        public void GetAlbumById_ShouldReturnAlbum_WhenAlbumExists()
        {
            var expectedAlbum = new Album { AlbumId = 2, Title = "21", Artist = "Adele", Genre = "Pop", ReleaseYear = 2011, Price = 9.99m, StockQuantity = 10 };
            _albumRepositoryMock.Setup(r => r.GetAlbumById(2)).Returns(expectedAlbum);

            var result = _albumService.GetAlbumById(2);

            Assert.IsNotNull(result);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedAlbum.AlbumId, result.AlbumId);
                Assert.AreEqual(expectedAlbum.Title, result.Title);
                Assert.AreEqual(expectedAlbum.Artist, result.Artist);
                Assert.AreEqual(expectedAlbum.Genre, result.Genre);
                Assert.AreEqual(expectedAlbum.ReleaseYear, result.ReleaseYear);
                Assert.AreEqual(expectedAlbum.Price, result.Price);
                Assert.AreEqual(expectedAlbum.StockQuantity, result.StockQuantity);
            });
        }
        [Test]
        public void GetAlbumById_ShouldReturnNull_WhenAlbumDoesNotExists()
        {
            _albumRepositoryMock.Setup(r => r.GetAlbumById(100)).Returns((Album?) null);

            var result = _albumService.GetAlbumById(100);

            Assert.IsNull(result);
        }
        [Test]
        public void GetAlbumById_ShouldInvokeGetAlbumByIdOnceFromRepositoryLayer()
        {
            var expectedAlbum = new Album { AlbumId = 2, Title = "21", Artist = "Adele", Genre = "Pop", ReleaseYear = 2011, Price = 9.99m, StockQuantity = 10 };
            _albumRepositoryMock.Setup(r => r.GetAlbumById(2)).Returns(expectedAlbum);

            _albumService.GetAlbumById(2);

            _albumRepositoryMock.Verify(r => r.GetAlbumById(2), Times.Once);
        }

        // -------------------------- AddNewAlbum Tests --------------------------------
        [Test]
        public void AddNewAlbum_ShouldReturnAlbum_WhenNotNull()
        {
            var newAlbum = new Album { AlbumId = 1, Title = "Thriller", Artist = "Michael Jackson", Genre = "Pop", ReleaseYear = 1982, Price = 11.99m, StockQuantity = 8 };
            _albumRepositoryMock.Setup(r => r.AddNewAlbum(newAlbum)).Returns(newAlbum);

            var result = _albumService.AddNewAlbum(newAlbum);

            Assert.IsNotNull(result);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(newAlbum.AlbumId, result.AlbumId);
                Assert.AreEqual(newAlbum.Title, result.Title);
                Assert.AreEqual(newAlbum.Artist, result.Artist);
                Assert.AreEqual(newAlbum.Genre, result.Genre);
                Assert.AreEqual(newAlbum.ReleaseYear, result.ReleaseYear);
                Assert.AreEqual(newAlbum.Price, result.Price);
                Assert.AreEqual(newAlbum.StockQuantity, result.StockQuantity);
            });
        }
    }
}