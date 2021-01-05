using MajoliFE.Data.Data;
using System.Collections.Generic;

namespace MajoliFE.Data.Interfaces
{
	public interface IInvoiceRepository : IRepository<Invoice>
	{
		List<Invoice> GetInvocesByCustomerId(int customerId);
	}
}
