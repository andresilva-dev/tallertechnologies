namespace Product.Infra.Interface
{
    public interface IProductRepository
    {
        Task<int> AddAsync(Domain.Product product);
        Task<IEnumerable<Domain.Product>> GetAllAsync();
        Task<Domain.Product> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<Domain.Product> UpdateAsync(Domain.Product product);
    }
}
