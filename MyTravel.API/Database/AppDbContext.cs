using Microsoft.EntityFrameworkCore;
using MyTravel.API.Models;

namespace MyTravel.API.Database
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {

        }

        public DbSet<TouristRoute> TouristRoutes { get; set; }
        public DbSet<TouristRoutePicture> TouristRoutePictures { get; set; }
    }
}
