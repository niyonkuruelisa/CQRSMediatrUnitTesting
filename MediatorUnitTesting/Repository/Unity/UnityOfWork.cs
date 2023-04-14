using MediatrUnitTesting.Abstractions;
using MediatrUnitTesting.Repository.Database;

namespace MediatrUnitTesting.Repository.Unity
{
	public class UnityOfWork : IUnitOfWork
	{
		private readonly UnitTestingDbContext unitTestingDbContext;

		public UnityOfWork(UnitTestingDbContext unitTestingDbContext) {
			this.unitTestingDbContext = unitTestingDbContext;
		}
		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await unitTestingDbContext.SaveChangesAsync(cancellationToken);
		}
	}

}
