using RecordShop.Controllers;
using RecordShop.DataModels;
using RecordShop.Services;
using Moq;
using Microsoft.AspNetCore.Mvc;
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

        [Test]
        public void GetAllAlbums_ReturnsOkResult()
        {
            _albumServicesMock.Setup(s => s.GetAllAlbums()).Returns(_albums);
            var result = _albumController.GetAllAlbums();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetAllAlbums_ShouldInvokeGetAllAlbumsFromServiceLayer()
        {
            _albumController.GetAllAlbums();
            _albumServicesMock.Verify(s => s.GetAllAlbums(), Times.Once);
        }
    }
}