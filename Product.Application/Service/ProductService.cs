using Product.Application.DTOs;
using Product.Application.Interface;
using Product.Infra.Interface;

namespace Product.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        
        public ProductService(IProductRepository productRepository) 
        {
            _productRepository = productRepository; 
        }

        public async Task<int> AddAsync(ProductRequest product)
        {
            Domain.Product newProduct = new() 
            { 
                Name = product.Name, 
                IsActive = true, 
                Price = product.Price 
            };
            
            return await _productRepository.AddAsync(newProduct);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductResponse() { Name = p.Name, Price = p.Price });
        }

        public async Task<ProductResponse> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            
            if (product is null)
                return default;

            return new ProductResponse()
            {
                Name = product.Name,
                Price = product.Price
            };
        }

        public async Task<ProductResponse> UpdateAsync(int id, ProductRequest product)
        {
            Domain.Product updateProduct = new()
            {
               Id = id,
               Name = product.Name,
               Price = product.Price,
               IsActive = true
            };

            updateProduct = await _productRepository.UpdateAsync(updateProduct);

            return new ProductResponse()
            {
                Name = updateProduct.Name,
                Price = updateProduct.Price
            };
        }
    }
}
