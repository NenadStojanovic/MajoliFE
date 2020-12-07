using MajoliFE.Data.Data;
using MajoliFE.Data.DbContexts;
using MajoliFE.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MajoliFE.Data.Repositories
{
	public class InvoiceItemRepository : Repository<InvoiceItem>, IInvoiceItemRepository
	{
		private readonly AppDbContext dbContext;
		public InvoiceItemRepository(AppDbContext dbContext) : base(dbContext)
		{
			this.dbContext = dbContext;
		}

		public IEnumerable<InvoiceItem> GetByInvoiceId(int invoiceId)
		{
			var result = dbContext.InvoiceItems.Where(x => x.InvoiceId == invoiceId).ToList();
			return result;
		}
	}
}
