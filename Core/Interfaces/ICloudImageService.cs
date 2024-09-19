using Core.Helpers;

namespace Core.Interfaces
{
    public interface ICloudImageService
    {
        Task<UploadImageResult> UploadImageAsync(byte[] fileData, string fileName);
        Task<List<UploadImageResult>> UploadImagesAsync(List<(byte[] FileData, string FileName)> files);
        Task<bool> DeleteImageAsync(string publicId);
        Task<bool> DeleteImagesAsync(List<string> publicIds);
    }
}
