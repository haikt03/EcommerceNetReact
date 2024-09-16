using Core.Entities;
using Core.Interfaces.IRepositories;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Author>> GetAllAsync(string? search = null)
        {
            var query = _context.Authors.AsQueryable().Search(search).OrderBy(a => a.FullName);

            return await query.ToListAsync();
        }
    }
}
