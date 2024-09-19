using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Params;
using Infrastructure.Data;
using Infrastructure.Extensions;

namespace Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Book>> GetAllAsync(BookParam bookParam)
        {
            var query = _context.Books.AsQueryable()
                                        .Search(bookParam.Search)
                                        .Filter(bookParam.Categories, bookParam.Authors, bookParam.Languages, bookParam.MinPrice, bookParam.MaxPrice)
                                        .Sort(bookParam.Sort);
            var result = await query.ToPagedListAsync(bookParam.PageSize, bookParam.PageIndex);

            return result;
        }

        public async Task<PagedList<Book>> GetAllByAuthor(PaginationParam paginationParam ,int authorId)
        {
            var result = await _context.Books.Where(b => b.AuthorId == authorId)
                .ToPagedListAsync(paginationParam.PageSize, paginationParam.PageIndex);
            return result;
        }

        public async Task<PagedList<Book>> GetAllByCategory(PaginationParam paginationParam ,int categoryId)
        {
            var result = await _context.Books.Where(b => b.CategoryId == categoryId)
                .ToPagedListAsync(paginationParam.PageSize, paginationParam.PageIndex);
            return result;
        }
    }
}
