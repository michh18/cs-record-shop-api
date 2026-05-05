namespace RecordShop.DataModels
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; } = "";
        public string Artist { get; set; } = "";
        public string Genre { get; set; } = "";
        public int ReleaseYear { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}

