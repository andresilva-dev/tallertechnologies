using Product.Application.DTOs;

namespace Product.Application.Interface
{
    public interface IProductService
    {
        Task<int> AddAsync(ProductRequest product);
        Task<IEnumerable<ProductResponse>> GetAllAsync();
        Task<ProductResponse> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<ProductResponse> UpdateAsync(int id, ProductRequest product);
    }
}
