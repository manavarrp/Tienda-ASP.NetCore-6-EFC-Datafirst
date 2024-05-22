using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> List();

        Task<Category> CreateCategory(Category entity);
        Task<Category> UpdateCategory(Category entity);
        Task<bool> DeleteCategory(int idCategory);
    }
}
