using System.ComponentModel.DataAnnotations;

namespace RecordShop.DataModels
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; } = "";
        public string Artist { get; set; } = "";
        public string Genre { get; set; } = "";
        [Range(1800, 2100)]
        public int ReleaseYear { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; } = "";
        public Album(int albumId, string title, string artist, string genre, int releaseYear, decimal price, int stockQuantity, string imageUrl) 
        {
            AlbumId = albumId;
            Title = title;
            Artist = artist;
            Genre = genre;
            ReleaseYear = releaseYear;
            Price = price;
            StockQuantity = stockQuantity;
            ImageUrl = imageUrl;
        }
    }
}

