using Core.Helpers;

namespace Core.Interfaces
{
    public interface ICloudImageService
    {
        Task<UploadImageResult> UploadImageAsync(UploadImageParam uploadParam);
        Task<bool> DeleteImageAsync(string publicId);
    }
}
