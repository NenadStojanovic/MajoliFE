using MajoliFE.Data.Data;
using MajoliFE.Data.DbContexts;
using MajoliFE.Data.Interfaces;

namespace MajoliFE.Data.Repositories
{
	public class VendorRepository : Repository<Vendor>, IVendorRepository
	{
		private readonly AppDbContext dbContext;
		public VendorRepository(AppDbContext dbContext) : base(dbContext)
		{
			this.dbContext = dbContext;
		}
	}
}
