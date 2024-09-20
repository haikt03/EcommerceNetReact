using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Extensions;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Author>> GetAllAsync(PaginationParam paginationParam,string? search = null)
        {
            var query = _context.Authors.AsQueryable().Search(search).OrderBy(a => a.FullName);
            var result = await query.ToPagedListAsync(paginationParam.PageSize, paginationParam.PageIndex);

            return result;
        }
    }
}
