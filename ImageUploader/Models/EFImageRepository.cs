using System.Linq;

namespace ImageUploader.Models
{
    public class EFImageRepository: IImageRepository
    {
        private ImageDbContext _context;
        public EFImageRepository(ImageDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Expression body member - get only with this syntax
        /// </summary>
        public IQueryable<Image> Images => _context.Images;

        public void SaveImage(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
        }
    }
}
