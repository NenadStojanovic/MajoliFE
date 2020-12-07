using MajoliFE.Data.Data;
using System.Collections.Generic;

namespace MajoliFE.Data.Interfaces
{
	public interface IInvoiceItemRepository : IRepository<InvoiceItem>
	{
		IEnumerable<InvoiceItem> GetByInvoiceId(int invoiceId);
	}
}
