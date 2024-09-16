using Core.Interfaces.IRepositories;
using Core.Interfaces.IRepositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            bookRepo = new BookRepository(_context);
            authorRepo = new AuthorRepository(_context);
            categoryRepo = new CategoryRepository(_context);
        }

        public IBookRepository bookRepo { get; private set; }

        public IAuthorRepository authorRepo { get; private set; }

        public ICategoryRepository categoryRepo { get; private set; }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
