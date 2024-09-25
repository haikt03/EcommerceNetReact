using API.Dtos.Book;
using API.Dtos.Image;
using Core.Entities;

namespace API.Extensions.Mappings
{
    public static class BookMappingExtension
    {
        public static BookDto ToDto(this Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Publisher = book.Publisher,
                PublishedYear = book.PublishedYear,
                Language = book.Language,
                Translator = book.Translator,
                ISBN = book.ISBN,
                Description = book.Description,
                Price = book.Price,
                QuantityInStock = book.QuantityInStock,
                CategoryName = book.Category?.Name,
                AuthorName = book.Author?.FullName,
                Images = book.Images?.Select(i => new ImageDto
                {
                    PublicId = i?.PublicId,
                    Url = i?.Url
                }).ToList()
            };
        }

        public static Book ToEntity(this BookUpsertDto bookDto)
        {
            return new Book
            {
                Title = bookDto.Title,
                Publisher = bookDto.Publisher,
                PublishedYear = bookDto.PublishedYear,
                Language = bookDto.Language,
                Translator = bookDto.Translator,
                ISBN = bookDto.ISBN,
                Description = bookDto.Description,
                Price = bookDto.Price,
                QuantityInStock = bookDto.QuantityInStock,
                CategoryId = bookDto.CategoryId,
                AuthorId = bookDto.AuthorId
            };
        }
    }
}
