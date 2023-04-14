using MediatrUnitTesting.Models;
using Microsoft.EntityFrameworkCore;

namespace MediatrUnitTesting.Repository.Database
{
	public class UnitTestingDbContext : DbContext
	{
		#region Contructor
		public UnitTestingDbContext(DbContextOptions<UnitTestingDbContext> options)
				: base(options)
		{
		
		}

		#endregion

		public DbSet<User> Users { get; set; }
	}
}
