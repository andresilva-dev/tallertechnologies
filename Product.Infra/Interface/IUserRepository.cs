using Product.Domain;

namespace Product.Infra.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
    }
} 