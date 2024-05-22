using SALESSYSTEM.BLL.Interfaces;
using SALESSYSTEM.DAL.Interfaces;
using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.BLL.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _repository = categoryRepository;
        }

        public async Task<List<Category>> List()
        {
            IQueryable<Category> query = await _repository.GetEntityQuery();
            return query.ToList();
        }


        public async Task<Category> CreateCategory(Category entity)
        {
            try
            {
                Category category = await _repository.Create(entity);
                if(category.IdCategory == 0)
                    throw new TaskCanceledException("No se pudo crear la categoria");

                return category;

            }catch(Exception ex)
            {
                throw;
            }
           
        }

        public async Task<Category> UpdateCategory(Category entity)
        {
            try
            {
                Category category = await _repository.Get(c=> c.IdCategory == entity.IdCategory);
                if (category?.IdCategory == null)
                    throw new TaskCanceledException("No encontrada la categoria");
                category.Description = entity.Description;
                category.IsActive = entity.IsActive;

                bool res = await _repository.Update(category);

                if (!res)
                    throw new TaskCanceledException("No se pudo editar la categoria");

                return category;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteCategory(int idCategory)
        {
            try
            {
                Category category = await _repository.Get(c => c.IdCategory == idCategory);
                if(category?.IdCategory ==null)
                    throw new TaskCanceledException("No encontrada la categoria");
                bool res = await _repository.Remove(category);

                return res;

            }
            catch (Exception ex)
            {
                throw;
            }
         
        }
    }
}
