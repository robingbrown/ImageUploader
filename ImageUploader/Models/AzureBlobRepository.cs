using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace ImageUploader.Models
{
    public class AzureBlobRepository : IImageRepository
    {
        public IQueryable<Image> Images => throw new NotImplementedException();
        public BlobContainerClient BlobContainer;

        public AzureBlobRepository()
        {
            string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            //Create a unique name for the container
            string containerName = "imageuploaderblobs" + Guid.NewGuid().ToString();

            // Create the container and return a container client object
            BlobContainer = blobServiceClient.CreateBlobContainer(containerName);
        }

        public void SaveImage(Image image)
        {
            BlobContainer.UploadBlob(image.ImageName, (BinaryData)image.ImageFile);
        }
    }
}
