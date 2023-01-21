using WebApiApplication.DAO;

namespace WebApiApplication.WebService
{
    public interface IProductRepository
    {
        Task<ProductCategory> GetCategoryAsync();
        Task AddProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
