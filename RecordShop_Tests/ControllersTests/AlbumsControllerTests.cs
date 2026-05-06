using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordShop.Controllers;
using RecordShop.DataModels;
using RecordShop.Services;
namespace RecordShop_Tests.ControllersTests
{
    public class AlbumsControllersTests
    {
        private Mock<IAlbumsService> _albumServicesMock;
        private AlbumsController _albumController;
        private IEnumerable<Album> _albums;

        [SetUp]
        public void Setup()
        {
            _albumServicesMock = new Mock<IAlbumsService>();
            _albumController = new AlbumsController(_albumServicesMock.Object);
            _albums = new List<Album>
            {
                new Album {AlbumId = 1, Title = "Thriller", Artist = "Michael Jackson", Genre = "Pop", ReleaseYear = 1982, Price = 11.99m, StockQuantity = 8 },
                new Album {AlbumId = 2, Title = "21", Artist = "Adele", Genre = "Pop", ReleaseYear = 2011, Price = 9.99m, StockQuantity = 10},
                new Album {AlbumId = 3, Title = "Back to Black", Artist = "Amy Winehouse", Genre = "Soul", ReleaseYear = 2006, Price = 10.99m, StockQuantity = 3},
                new Album {AlbumId = 4, Title = "Abbey Road", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1969, Price = 12.99m, StockQuantity = 5},
                new Album {AlbumId = 5, Title = "To Pimp a Butterfly", Artist = "Kendrick Lamar", Genre = "Hip-Hop", ReleaseYear = 2015, Price = 13.49m, StockQuantity = 4}
            };
        }

        // --------------------- GetAllAlbums Tests --------------------------------------
        [Test]
        public void GetAllAlbums_ReturnsOkResultWithAlbumsList()
        {
            _albumServicesMock.Setup(s => s.GetAllAlbums()).Returns(_albums);

            var result = _albumController.GetAllAlbums();
            var okResult = result as OkObjectResult;
            var returnedAlbums = okResult.Value as IEnumerable<Album>;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(returnedAlbums);
            Assert.That(returnedAlbums.Count(), Is.EqualTo(5));
        }
        [Test]
        public void GetAllAlbums_ShouldInvokeGetAllAlbumsFromServiceLayer()
        {
            _albumServicesMock.Setup(s => s.GetAllAlbums()).Returns(_albums);

            _albumController.GetAllAlbums();

            _albumServicesMock.Verify(s => s.GetAllAlbums(), Times.Once);
        }

        // --------------------- GetAlbumById Tests --------------------------------------
        [Test]
        public void GetAlbumById_ReturnsOkResultWithAlbum_WhenAlbumExists()
        {
            var album = new Album { AlbumId = 1, Title = "Thriller", Artist = "Michael Jackson", Genre = "Pop", ReleaseYear = 1982, Price = 11.99m, StockQuantity = 8 };
            _albumServicesMock.Setup(s => s.GetAlbumById(1)).Returns(album);

            var result = _albumController.GetAlbumById(1);
            var okResult = result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsNotNull(okResult);
            Assert.AreEqual(album, okResult.Value);
        }
        [Test]
        public void GetAlbumById_ReturnsNotFound_WhenAlbumDoesNotExist()
        {
            _albumServicesMock.Setup(s => s.GetAlbumById(1)).Returns((Album?)null);

            var result = _albumController.GetAlbumById(1);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
        [Test]
        public void GetAlbumById_ShouldInvokeGetAlbumByIdFromServiceLayer()
        {
            _albumServicesMock.Setup(s => s.GetAlbumById(1)).Returns(_albums.First(s => s.AlbumId == 1));

            _albumController.GetAlbumById(1);

            _albumServicesMock.Verify(s => s.GetAlbumById(1), Times.Once);
        }

        // --------------------- PostNewAlbum Tests --------------------------------------
        [Test]
        public void PostNewAlbum_ReturnsCreatedAtActionResultWithAlbum()
        {
            var newAlbum = new Album { AlbumId = 1, Title = "Thriller", Artist = "Michael Jackson", Genre = "Pop", ReleaseYear = 1982, Price = 11.99m, StockQuantity = 8 };
            _albumServicesMock.Setup(s => s.AddNewAlbum(newAlbum)).Returns(newAlbum);

            var result = _albumController.PostNewAlbum(newAlbum);
            var createdResult = result as CreatedAtActionResult;

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(newAlbum, createdResult.Value);
        }
        [Test]
        public void PostNewAlbum_ReturnsBadRequest_WhenAlbumIsNull()
        {
            Album? newAlbum = null;

            var result = _albumController.PostNewAlbum(newAlbum);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        public void PostNewAlbum_ReturnsBadRequest_WhenSomeAlbumInfoIsInvalid()
        {
            Album badAlbum = new Album { AlbumId = 1, Title = " ", Artist = " ", Genre = " ", ReleaseYear = 1982, Price = 11.99m, StockQuantity = 8 };

            var result = _albumController.PostNewAlbum(badAlbum);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        public void PostNewAlbum_ShouldInvokeAddNewAlbumFromServiceLayer()
        {
            var newAlbum = new Album { AlbumId = 1, Title = "Thriller", Artist = "Michael Jackson", Genre = "Pop", ReleaseYear = 1982, Price = 11.99m, StockQuantity = 8 };
            _albumServicesMock.Setup(s => s.AddNewAlbum(newAlbum)).Returns(newAlbum);

            _albumController.PostNewAlbum(newAlbum);

            _albumServicesMock.Verify(s => s.AddNewAlbum(newAlbum), Times.Once);
        }
        // ------------------------ UpdateAlbum Tests ----------------------------------------------------
        [Test]
        public void UpdateAlbum_ReturnsOkResultWithUpdatedAlbum() 
        {
            var updatedAlbum = new Album { AlbumId = 4, Title = "Rumours", Artist = "Fleetwood Mac", Genre = "Rock", ReleaseYear = 1977, Price = 10.49m, StockQuantity = 7 };
            
            _albumServicesMock.Setup(s => s.UpdateAlbum(4, updatedAlbum)).Returns(updatedAlbum);

            var result = _albumController.UpdateAlbum(4, updatedAlbum);
            var okResult = result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsNotNull(okResult);
            Assert.AreEqual(updatedAlbum, okResult.Value);

        }
        [Test]
        public void UpdateAlbum_ReturnsBadRequest_WhenAlbumIsNull()
        {
            var result = _albumController.UpdateAlbum(1, (Album?)null);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var badRequest = result as BadRequestObjectResult;
            Assert.AreEqual("Album cannot be null", badRequest.Value);
        }
        [Test]
        public void UpdateAlbum_ReturnsBadRequest_WhenAlbumIsInvalid()
        {
            var updatedAlbum = new Album { AlbumId = 4, Title = " ", Artist = "Fleetwood Mac", Genre = "Rock", ReleaseYear = 1977, Price = 10.49m, StockQuantity = 7 };

            var result = _albumController.UpdateAlbum(4, updatedAlbum);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var badRequest = result as BadRequestObjectResult;
            Assert.AreEqual("Invalid album info", badRequest.Value);
        }
        [Test]
        public void UpdateAlbum_ReturnsBadRequest_WhenAlbumIdDoesNotExist()
        {
            var updatedAlbum = new Album { AlbumId = 4, Title = "Rumours", Artist = "Fleetwood Mac", Genre = "Rock", ReleaseYear = 1977, Price = 10.49m, StockQuantity = 7 };

            _albumServicesMock.Setup(s => s.UpdateAlbum(4, updatedAlbum)).Returns((Album)null);
            var result = _albumController.UpdateAlbum(4, updatedAlbum);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);

            var notFound = result as NotFoundObjectResult;
            Assert.AreEqual("AlbumId not found", notFound.Value);
        }
        [Test]
        public void UpdatedAlbum_ShouldInvokeUpdateAlbumFromServiceLayer()
        {
            var updatedAlbum = new Album { AlbumId = 4, Title = "Rumours", Artist = "Fleetwood Mac", Genre = "Rock", ReleaseYear = 1977, Price = 10.49m, StockQuantity = 7 };
            _albumServicesMock.Setup(s => s.UpdateAlbum(4, updatedAlbum)).Returns(updatedAlbum);

            _albumController.UpdateAlbum(4, updatedAlbum);

            _albumServicesMock.Verify(s => s.UpdateAlbum(4, updatedAlbum), Times.Once);
        }
    }
}