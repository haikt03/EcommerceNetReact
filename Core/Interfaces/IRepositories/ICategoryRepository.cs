using Core.Entities;
using Core.Interfaces.IRepositories;

namespace Core.Interfaces.IRepositories
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        Task<List<Category>> GetAllAsync(string? search = null);
        Task<List<Category>> GetAllHierarchyAsync();
    }
}
