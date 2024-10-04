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
                Name = book.Name,
                Translator = book.Translator,
                Publisher = book.Publisher,
                PublishedYear = book.PublishedYear,
                Language = book.Language,
                Weight = book.Weight,
                NumberOfPages = book.NumberOfPages,
                Form = book.Form,
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

        public static Book ToEntity(this BookUpsertDto bookDto, Book? book = null)
        {
            if (book == null)
            {
                return new Book
                {
                    Name = bookDto.Name,
                    Translator = bookDto.Translator,
                    Publisher = bookDto.Publisher,
                    PublishedYear = bookDto.PublishedYear,
                    Language = bookDto.Language,
                    Weight = bookDto.Weight,
                    NumberOfPages = bookDto.NumberOfPages,
                    Form = bookDto.Form,
                    Description = bookDto.Description,
                    Price = bookDto.Price,
                    QuantityInStock = bookDto.QuantityInStock,
                    CategoryId = bookDto.CategoryId,
                    AuthorId = bookDto.AuthorId
                };
            }

            book.Name = bookDto.Name;
            book.Translator = bookDto.Translator;
            book.Publisher = bookDto.Publisher;
            book.PublishedYear = bookDto.PublishedYear;
            book.Language = bookDto.Language;
            book.Weight = bookDto.Weight;
            book.NumberOfPages = bookDto.NumberOfPages;
            book.Form = bookDto.Form;
            book.Description = bookDto.Description;
            book.Price = bookDto.Price;
            book.QuantityInStock = bookDto.QuantityInStock;
            book.CategoryId = bookDto.CategoryId;
            book.AuthorId = bookDto.AuthorId;
            return book;
        }
    }
}
