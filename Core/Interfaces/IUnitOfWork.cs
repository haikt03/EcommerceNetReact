
using Core.Interfaces;

namespace Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IBookRepository bookRepo { get; }
        IAuthorRepository authorRepo { get; }
        ICategoryRepository categoryRepo { get; }
        Task<bool> CompleteAsync();
    }
}
