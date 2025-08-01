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

        public async Task<int> AddAsync(Domain.Product product)
        {
            return await _productRepository.AddAsync(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public IEnumerable<Domain.Product> GetAll()
        {
            return _productRepository.GetAll();   
        }

        public async Task<Domain.Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Domain.Product> UpdateAsync(Domain.Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }
    }
}
