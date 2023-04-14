using MediatrUnitTesting.Models;

namespace MediatrUnitTesting.Abstractions
{
    public interface IUserRepository
    {
        void Create(User user);
        void UpdateAsync(User user);
        void DeleteAsync(User user);
        User GetUserById(Guid userId);
    }
}
