using Microsoft.EntityFrameworkCore;
using VinylShop.Api.Persistence.Entities;

namespace VinylShop.Api.Persistence
{
    public class VinylShopContext : DbContext
    {
        public DbSet<Album> Albums => Set<Album>();
        public DbSet<Song> Songs => Set<Song>();

        public VinylShopContext(DbContextOptions<VinylShopContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(
                new AlbumConfig());
            modelBuilder.ApplyConfiguration(
                new SongConfig());
        }
    }
}
