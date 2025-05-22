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
        public DbSet<Client> Clients { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = "admin01",
                    Password = "MusicRadioAdmin",
                    Name = "Administrador",
                    Mail = "admin@example.com",
                    Direction = "Calle Falsa 123",
                    Phone = "1234567890"
                }
            );

            modelBuilder.Entity<AlbumSet>().HasData(
                new AlbumSet
                {
                    Id = 1,
                    Name = "Steal this album",
                    Price = 9.99f
                },
                new AlbumSet
                {
                    Id = 2,
                    Name = "Toxixity",
                    Price = 14.99f
                }
            );

            modelBuilder.Entity<SongSet>().HasData(
                new SongSet
                {
                    Id = 1,
                    Name = "Chop Suey",
                    Album_Id = 2
                },
                new SongSet
                {
                    Id = 2,
                    Name = "Bounce",
                    Album_Id = 1
                }
            );

            modelBuilder.Entity<PurchaseDetail>().HasData(
                new PurchaseDetail
                {
                    Id = 1,
                    Client_Id = "admin01",
                    Album_Id = 1,
                    PurchaseDate = new DateTime(2024, 1, 1),
                    Total = 9.99f
                },
                new PurchaseDetail
                {
                    Id = 2,
                    Client_Id = "admin01",
                    Album_Id = 2,
                    PurchaseDate = new DateTime(2024, 1, 1),
                    Total = 14.99f
                }
            );

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .Property(c => c.Password)
                .HasDefaultValue("MusicRadio");
                }


    }



}
