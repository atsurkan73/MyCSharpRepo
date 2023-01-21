using Microsoft.EntityFrameworkCore;
using WebApiApplication.DAO;
//using WebApplication2.Controllers;

namespace WebApiApplication.WebService
{
    public class ProductRepository : IProductRepository
    {
        private readonly NewDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        //        public ProductRepository(NewDbContext context) => _context = context;

        public ProductRepository(ILogger<ProductRepository> logger, NewDbContext dBcontext)
        {
            _logger = logger;
            _context = dBcontext;
        }


        public async Task<ProductCategory> GetCategoryAsync()
        {
            _logger.Log(LogLevel.Information, $"Get reguest of Product_category has been sent on server");
            return await _context.Set<ProductCategory>()
              .Include(category => category.Product)
              .FirstAsync();
        }

        public async Task AddProductAsync(Product product)
        {

            if (product.ProductName.Length < 5)
                _logger.LogError($"Too small length of ProductName{product.ProductName}");

            else if (_context.Products.Any(p => p.ProductId.Equals(product.ProductId)))
                _logger.LogError($"Id {product.ProductId} is already avalaible in Product list");

            else if (product.ProductName != "")
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                _logger.Log(LogLevel.Information, $"Product {product.ProductName} has been added");
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var availableProduct = await _context.Set<Product>()
                .FirstOrDefaultAsync(product => product.ProductId == id);

           
            _logger.Log(LogLevel.Information, $"Product 'id = {id}' has been removed");

            return await ValidateAndProcess (availableProduct, () => _context.Products.Remove(availableProduct!));
        }

        public async Task<bool> ValidateAndProcess(Product? prod, Delegate method)
        {
            if (prod is null) return false;
            method.DynamicInvoke();
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
