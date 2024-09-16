using Core.Entities;
using Core.Interfaces.IRepositories;

namespace Core.Interfaces.IRepositories
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<List<Author>> GetAllAsync(string? search = null);
    }
}
