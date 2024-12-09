namespace VinylShop.Client.Features.Home
{
    public class Song
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public Album? Album { get; set; }
        public int? AlbumId { get; set; }
    }
}
