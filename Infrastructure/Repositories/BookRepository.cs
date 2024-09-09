using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }
    }
}
