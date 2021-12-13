using System.Linq;

namespace ImageUploader.Models
{
    /// <summary>
    /// This interface will allow us to subsititute a different back end depending on our needs
    /// </summary>
    public interface IImageRepository
    {
        IQueryable<Image> Images { get; }
        void SaveImage(Image image);
    }
}
