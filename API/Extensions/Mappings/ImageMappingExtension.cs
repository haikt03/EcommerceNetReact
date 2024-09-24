using API.Dtos.Image;
using Core.Entities;

namespace API.Extensions.Mappings
{
    public static class ImageMappingExtension
    {
        public static ImageDto ToDto(this Image image)
        {
            return new ImageDto
            {
                Id = image.Id,
                PublicId = image.PublicId,
                Url = image.Url,
                BookId = image.BookId,
                AuthorId = image.AuthorId
            };
        }
    }
}
