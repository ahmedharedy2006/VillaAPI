using Microsoft.EntityFrameworkCore;
using VillaAPI.Models;

namespace VillaAPI.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(

                  new Villa
                  {
                      Id = 1,
                      Name = "Royal Villa",
                      Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                      ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa3.jpg",
                      Rate = 200,
                      CreatedDate = DateTime.Now
                  },
              new Villa
              {
                  Id = 2,
                  Name = "Premium Pool Villa",
                  Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa1.jpg",
                  Rate = 300,
                  CreatedDate = DateTime.Now
              },
              new Villa
              {
                  Id = 3,
                  Name = "Luxury Pool Villa",
                  Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa4.jpg",
                  Rate = 400,
                  CreatedDate = DateTime.Now
              },
              new Villa
              {
                  Id = 4,
                  Name = "Diamond Villa",
                  Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa5.jpg",
                   Rate = 550,
                  CreatedDate = DateTime.Now
              }
                );
        }
    }
}
