using Microsoft.EntityFrameworkCore;
using SALESSYSTEM.BLL.Interfaces;
using SALESSYSTEM.DAL.Interfaces;
using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.BLL.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repository;

        public ProductService(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<List<Product>> List()
        {
            IQueryable<Product> products = await _repository.GetEntityQuery();
            return products.Include(c =>c.IdCategoryNavigation).ToList();
        }

        public async Task<Product> CreateProduct(Product entity)
        {
            Product productExist = await _repository.Get(p=>p.Barcode == entity.Barcode);
            if (productExist != null)
                throw new TaskCanceledException("El codigo de barra ya existe");
           
            try
            {
                Product product = await _repository.Create(entity);
                if (product.IdProduct == 0)
                    throw new TaskCanceledException("No se pudo crear el producto");

                IQueryable<Product> query = await _repository.GetEntityQuery(p=>p.IdProduct==product.IdProduct);
                product = query.Include(c=>c.IdCategoryNavigation).First();


                return product;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Product> UpdateProduct(Product entity)
        {
            Product productExist = await _repository.Get(p => p.Barcode == entity.Barcode && p.IdProduct != entity.IdProduct);
            if (productExist != null)
                throw new TaskCanceledException("El codigo de barra ya existe");

            try
            {
                IQueryable<Product> query = await _repository.GetEntityQuery(p => p.IdProduct == entity.IdProduct);
                Product product = query.First();

                product.Barcode = entity.Barcode;
                product.Brand = entity.Brand;
                product.Description = entity.Description;
                product.IdCategory = entity.IdCategory;
                product.Stock = entity.Stock;
                product.Price = entity.Price;
                product.IsActive = entity.IsActive;
                

                bool res = await _repository.Update(product);

                if (!res)
                    throw new TaskCanceledException("No se pudo editar el producto");

                Product updateProduct = query.Include(c => c.IdCategoryNavigation).First();

                return product;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> DeleteProduct(int idProduct)
        {
            try
            {
                Product product = await _repository.Get(c => c.IdProduct == idProduct);
                if (product?.IdCategory == null)
                    throw new TaskCanceledException("No encontrada la categoria");
                bool res = await _repository.Remove(product);

                return res;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

      
    }
}
