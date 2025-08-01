namespace Product.Infra.Interface
{
    public interface IProductRepository
    {
        Task<int> AddAsync(Domain.Product product);
        IEnumerable<Domain.Product> GetAll();
        Task<Domain.Product> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<Domain.Product> UpdateAsync(Domain.Product product);
    }
}
