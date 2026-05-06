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
                new Album (1, "Thriller", "Michael Jackson", "Pop", 1982, 11.99m, 8 ),
                new Album (2, "21", "Adele", "Pop", 2011, 9.99m, 10),
                new Album (3, "Back to Black", "Amy Winehouse", "Soul", 2006, 10.99m, 3),
                new Album (4, "Abbey Road", "The Beatles", "Rock", 1969, 12.99m, 5),
                new Album (5, "To Pimp a Butterfly", "Kendrick Lamar", "Hip-Hop", 2015, 13.49m, 4)
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
            var expectedAlbum = new Album (2, "21", "Adele", "Pop", 2011, 9.99m, 10);
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
            var expectedAlbum = new Album (2, "21", "Adele", "Pop", 2011, 9.99m, 10);
            _albumRepositoryMock.Setup(r => r.GetAlbumById(2)).Returns(expectedAlbum);

            _albumService.GetAlbumById(2);

            _albumRepositoryMock.Verify(r => r.GetAlbumById(2), Times.Once);
        }

        // -------------------------- AddNewAlbum Tests --------------------------------
        [Test]
        public void AddNewAlbum_ShouldReturnAlbum_WhenNotNull()
        {
            var newAlbum = new Album (1,"Thriller", "Michael Jackson", "Pop", 1982, 11.99m, 8);
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

        [Test]
        public void AddNewAlbum_ShouldInvokeAddNewAlbumOnceFromRepositoryLayer()
        {
            var newAlbum = new Album (2, "21", "Adele", "Pop", 2011, 9.99m, 10);
            _albumRepositoryMock.Setup(r => r.AddNewAlbum(newAlbum)).Returns(newAlbum);

            _albumService.AddNewAlbum(newAlbum);

            _albumRepositoryMock.Verify(r => r.AddNewAlbum(newAlbum), Times.Once);
        }
        // ------------------------ UpdateAlbum Tests ----------------------------------------------------
        [Test]
        public void UpdateAlbum_UpdatesAllFields_WhenAlbumExists()
        {
            var updated = new Album (4, "Rumours", "Fleetwood Mac", "Rock", 1977, 10.49m, 7);

            _albumRepositoryMock.Setup(r => r.UpdateAlbum(4, updated)).Returns(updated);

            var result = _albumService.UpdateAlbum(4, updated);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(4, result.AlbumId);
                Assert.AreEqual(updated.Title, result.Title);
                Assert.AreEqual(updated.Artist, result.Artist);
                Assert.AreEqual(updated.Genre, result.Genre);
                Assert.AreEqual(updated.ReleaseYear, result.ReleaseYear);
                Assert.AreEqual(updated.Price, result.Price);
                Assert.AreEqual(updated.StockQuantity, result.StockQuantity);
            });
        }
        [Test]
        public void UpdateAlbum_DoesNotUpdate_WhenAlbumDoesNotExists()
        {
            var updated = new Album (4, "Rumours", "Fleetwood Mac", "Rock", 1977, 10.49m, 7);

            _albumRepositoryMock.Setup(r => r.GetAlbumById(6)).Returns((Album)null);

            var result = _albumService.UpdateAlbum(6, updated);

            Assert.IsNull(result);
        }
        [Test]
        public void UpdatedAlbum_ShouldInvokeUpdateAlbumFromRepositoryLayer()
        {
            var updatedAlbum = new Album (4, "Rumours", "Fleetwood Mac", "Rock", 1977, 10.49m, 7);
            _albumRepositoryMock.Setup(r => r.UpdateAlbum(4, updatedAlbum)).Returns(updatedAlbum);

            _albumService.UpdateAlbum(4, updatedAlbum);

            _albumRepositoryMock.Verify(r => r.UpdateAlbum(4, updatedAlbum), Times.Once);
        }
        // --------------------------- DeleteAlbumById Tests ----------------------------------
    }
}