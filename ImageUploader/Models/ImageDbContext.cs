using Microsoft.EntityFrameworkCore;

namespace ImageUploader.Models
{
    public class ImageDbContext : DbContext
    {
        public ImageDbContext(DbContextOptions<ImageDbContext> options) : base(options)
        { }

        public DbSet<Image> Images { get; set; }
    }
}
