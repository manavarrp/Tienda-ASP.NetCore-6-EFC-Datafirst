using SALESSYSTEM.Domain.Entities;

namespace SALESSYSTEM.BLL.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> List();
        Task<Product> CreateProduct(Product entity);
        Task<Product> UpdateProduct(Product entity);
        Task<bool> DeleteProduct(int idProduct);
    }
}
