using API.Dtos.Author;
using API.Dtos.Image;
using Core.Entities;

namespace API.Extensions.Mappings
{
    public static class AuthorMappingExtension
    {
        public static AuthorDto ToDto(this Author author)
        {
            return new AuthorDto
            {
                Id = author.Id,
                FullName = author.FullName,
                Biography = author.Biography,
                Country = author.Country,
                Books = author.Books?.Select(b => b.ToDto()).ToList(),
                Image = new ImageDto
                {
                    PublicId = author?.Image?.PublicId,
                    Url = author?.Image?.Url
                }
            };
        }

        public static Author ToEntity(this AuthorUpsertDto authorDto, Author? author = null)
        {
            if (author == null)
                return new Author
                {
                    FullName = authorDto.FullName,
                    Biography = authorDto.Biography,
                    Country = authorDto.Country
                };

            author.FullName = authorDto.FullName;
            author.Biography = authorDto.Biography;
            author.Country = authorDto.Country;
            return author;
        }
    }
}
