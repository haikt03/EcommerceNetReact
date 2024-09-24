using API.Dtos.Author;
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
                Image = author.Image?.ToDto(),
                Books = author.Books?.Select(b => b.ToDto()).ToList()
            };
        }

        public static Author ToEntity(this AuthorRequestDto authorDto)
        {
            return new Author
            {
                FullName = authorDto.FullName,
                Biography = authorDto.Biography,
                Country = authorDto.Country
            };
        }
    }
}
