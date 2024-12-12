using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VinylShop.Api.Persistence.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public int? AlbumId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public Album? Album { get; set; } = default!;
    }

    public class SongConfig : IEntityTypeConfiguration<Song>
    {
        public void Configure (EntityTypeBuilder<Song> builder)
        {
            builder.Property(x => x.AlbumId).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Text).IsRequired();
        }
    }
}
