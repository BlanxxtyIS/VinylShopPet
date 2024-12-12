
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VinylShop.Api.Persistence.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public int TimeInMinutes { get; set; }
        public string TimeFormatted => $"{TimeInMinutes / 60}h {TimeInMinutes % 60}m";

        public int Rating { get; set; }
        public IEnumerable<Song> Song { get; set; } = default!;
    }

    public class AlbumConfig : IEntityTypeConfiguration<Album> 
    {
        public void Configure (EntityTypeBuilder<Album> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Image).IsRequired();
            builder.Property(x => x.AuthorName).IsRequired();
            builder.Property(x => x.TimeInMinutes).IsRequired();
            builder.Property(x => x.Rating).IsRequired();
        }
    }
}
