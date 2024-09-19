using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Core.Helpers;

namespace Infrastructure.Services
{
    public class CloudImageService : ICloudImageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudImageService(IConfiguration config)
        {
            var account = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<UploadImageResult> UploadImageAsync(byte[] fileData, string fileName)
        {
            using (var stream = new MemoryStream(fileData))
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, stream),
                    PublicId = fileName
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                return new UploadImageResult
                {
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.SecureUrl.ToString()
                }; ;
            }
        }

        public async Task<List<UploadImageResult>> UploadImagesAsync(List<(byte[] FileData, string FileName)> files)
        {
            var uploadResults = new List<UploadImageResult>();
            foreach (var file in files)
            {
                var uploadResult = await UploadImageAsync(file.FileData, file.FileName);
                uploadResults.Add(uploadResult);
            }
            return uploadResults;
        }

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deletionParams);

            return result.Result == "ok";
        }

        public async Task<bool> DeleteImagesAsync(List<string> publicIds)
        {
            var allDeleted = true;
            foreach (var publicId in publicIds)
            {
                var deleted = await DeleteImageAsync(publicId);
                if (!deleted) allDeleted = false;
            }
            return allDeleted;
        }
    }
}
