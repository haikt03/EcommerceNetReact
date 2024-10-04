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

        public async Task<UploadImageResult> UploadImageAsync(UploadImageParam uploadParam)
        {
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(uploadParam.FileName, uploadParam.FileStream),
                Folder = "EcommerceNetReact"
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return new UploadImageResult
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUrl.ToString()
            };
        }

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deletionParams);

            return result.Result == "ok";
        }
    }
}
