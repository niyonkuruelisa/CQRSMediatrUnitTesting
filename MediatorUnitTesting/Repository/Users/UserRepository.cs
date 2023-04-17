using MediatrUnitTesting.Abstractions;
using MediatrUnitTesting.Models;
using MediatrUnitTesting.Repository.Database;

namespace MediatrUnitTesting.Repository.Users
{
	public class UserRepository : IUserRepository
	{
		private readonly UnitTestingDbContext dbContext;

		public UserRepository(UnitTestingDbContext dbContext) => this.dbContext = dbContext;

		public bool IsEmailUnique(string email) => !dbContext.Users.Any(user => user.Email == email);

		public void Create(User user) => dbContext.Users.AddAsync(user);
		public void DeleteAsync(User user) => dbContext.Users.Remove(user);

		public User GetUserById(Guid userId) => dbContext.Users.FirstOrDefault(user => user.Id == userId);

		public void UpdateAsync(User user) => dbContext.Users.Update(user);
	}
}
