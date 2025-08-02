using Microsoft.EntityFrameworkCore;
using Product.Infra.Context;
using Product.Infra.Interface;

namespace Product.Infra.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _productDbContext;

        public ProductRepository(ProductDbContext context) 
        {
            _productDbContext = context;
        }

        public async Task<int> AddAsync(Domain.Product product)
        {
            await _productDbContext.Products.AddAsync(product);
            await _productDbContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var productToDelete = await _productDbContext.Products.FirstOrDefaultAsync(x => x.IsActive && x.Id == id);

            if (productToDelete is null)
                throw new ApplicationException($"Don't exists a product to id: {id}");

            productToDelete.IsActive = false;
            _productDbContext.Update(productToDelete);

            await _productDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Domain.Product>> GetAllAsync()
        {
            var products = await _productDbContext.Products.Where(x => x.IsActive).ToListAsync();
            return products;
        }

        public async Task<Domain.Product> GetByIdAsync(int id)
        {
            var product = await _productDbContext.Products.FirstOrDefaultAsync(x => x.IsActive && x.Id == id );
            return product;
        }

        public async Task<Domain.Product> UpdateAsync(Domain.Product product)
        {
            var existsProduct = await _productDbContext.Products.AnyAsync(x => x.IsActive && x.Id == product.Id);

            if (!existsProduct)
                throw new ApplicationException($"Don't exists a product to id: {product.Id}");

            _productDbContext.Products.Update(product);
            await _productDbContext.SaveChangesAsync();

            return product;
        }
    }
}
