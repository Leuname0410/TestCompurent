using Microsoft.EntityFrameworkCore;
using TestCompurent.Models.Entities;

namespace TestCompurent.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {
            
        }

        public DbSet<AlbumSet> AlbumsSet { get; set; }
        public DbSet<SongSet> SongsSet  { get; set; }
    }
}
