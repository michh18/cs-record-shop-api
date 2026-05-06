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
                new Album (1, "Thriller", "Michael Jackson", "Pop", 1982, 11.99m, 8),
                new Album (2, "21", "Adele", "Pop", 2011, 9.99m, 10),
                new Album (3, "Back to Black", "Amy Winehouse", "Soul", 2006, 10.99m, 3),
                new Album (4, "Abbey Road", "The Beatles", "Rock", 1969, 12.99m, 5),
                new Album (5, "To Pimp a Butterfly", "Kendrick Lamar", "Hip-Hop", 2015, 13.49m, 4)
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
            var album = new Album (1, "Thriller", "Michael Jackson", "Pop", 1982, 11.99m, 8);
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
            var newAlbum = new Album (3, "Thriller", "Michael Jackson", "Pop", 1982, 11.99m, 8);
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
            _albumServicesMock.Verify(s => s.AddNewAlbum(newAlbum), Times.Never);
        }
        [Test]
        public void PostNewAlbum_ReturnsBadRequest_WhenSomeAlbumInfoIsInvalid()
        {
            Album badAlbum = new Album (7, " ", " ", " ", 1982, 11.99m, 8);

            var result = _albumController.PostNewAlbum(badAlbum);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            _albumServicesMock.Verify(s => s.AddNewAlbum(badAlbum), Times.Never);
        }
        [Test]
        public void PostNewAlbum_ShouldInvokeAddNewAlbumFromServiceLayer()
        {
            var newAlbum = new Album (1, "Thriller", "Michael Jackson", "Pop", 1982, 11.99m, 8);
            _albumServicesMock.Setup(s => s.AddNewAlbum(newAlbum)).Returns(newAlbum);

            _albumController.PostNewAlbum(newAlbum);

            _albumServicesMock.Verify(s => s.AddNewAlbum(newAlbum), Times.Once);
        }
        // ------------------------ UpdateAlbum Tests ----------------------------------------------------
        [Test]
        public void UpdateAlbum_ReturnsOkResultWithUpdatedAlbum() 
        {
            var updatedAlbum = new Album (4, "Rumours", "Fleetwood Mac", "Rock", 1977, 10.49m, 7);
            
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
            _albumServicesMock.Verify(s => s.UpdateAlbum(1, (Album?)null), Times.Never);
        }
        [Test]
        public void UpdateAlbum_ReturnsBadRequest_WhenAlbumIsInvalid()
        {
            var updatedAlbum = new Album (4, " ", "Fleetwood Mac", "Rock", 1977, 10.49m, 7);

            var result = _albumController.UpdateAlbum(4, updatedAlbum);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var badRequest = result as BadRequestObjectResult;
            Assert.AreEqual("Invalid album info", badRequest.Value);
            _albumServicesMock.Verify(s => s.UpdateAlbum(1, updatedAlbum), Times.Never);
        }
        [Test]
        public void UpdateAlbum_ReturnsBadRequest_WhenAlbumIsNotFound()
        {
            var updatedAlbum = new Album (4, "Rumours", "Fleetwood Mac", "Rock", 1977, 10.49m, 7);

            _albumServicesMock.Setup(s => s.UpdateAlbum(4, updatedAlbum)).Returns((Album)null);
            var result = _albumController.UpdateAlbum(4, updatedAlbum);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);

            var notFound = result as NotFoundObjectResult;
            Assert.AreEqual("AlbumId not found", notFound.Value);
        }
        [Test]
        public void UpdatedAlbum_ShouldInvokeUpdateAlbumFromServiceLayer()
        {
            var updatedAlbum = new Album (4,"Rumours", "Fleetwood Mac", "Rock", 1977, 10.49m, 7);
            _albumServicesMock.Setup(s => s.UpdateAlbum(4, updatedAlbum)).Returns(updatedAlbum);

            _albumController.UpdateAlbum(4, updatedAlbum);

            _albumServicesMock.Verify(s => s.UpdateAlbum(4, updatedAlbum), Times.Once);
        }
        // ----------------------------------- DeleteAlbumById tests -------------------------------------------------
        [Test]
        public void DeleteAlbumById_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            _albumServicesMock.Setup(s => s.DeleteAlbumById(4)).Returns(true);

            var result = _albumController.DeleteAlbumById(4);

            Assert.IsInstanceOf<NoContentResult>(result);

            _albumServicesMock.Verify(s => s.DeleteAlbumById(4), Times.Once);
        }
        [Test]
        public void DeleteAlbumById_ReturnsNotFound_WhenAlbumDoesNotExist()
        {
            _albumServicesMock.Setup(s => s.DeleteAlbumById(4)).Returns(false);

            var result = _albumController.DeleteAlbumById(4);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);

            var notFound = result as NotFoundObjectResult;
            Assert.AreEqual("Album not found", notFound.Value);
        }
        [Test]
        public void DeleteAlbumById_CallsServiceOnce()
        {
            _albumServicesMock.Setup(s => s.DeleteAlbumById(4)).Returns(true);

            _albumController.DeleteAlbumById(4);

            _albumServicesMock.Verify(s => s.DeleteAlbumById(4), Times.Once);
        }
    }
}