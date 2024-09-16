
using Core.Interfaces.IRepositories;

namespace Core.Interfaces.IRepositories
{
    public interface IUnitOfWork: IDisposable
    {
        IBookRepository bookRepo { get; }
        IAuthorRepository authorRepo { get; }
        ICategoryRepository categoryRepo { get; }
        Task<bool> Complete();
    }
}
