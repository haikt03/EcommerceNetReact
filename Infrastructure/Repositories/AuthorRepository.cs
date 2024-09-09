using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }
    }
}
