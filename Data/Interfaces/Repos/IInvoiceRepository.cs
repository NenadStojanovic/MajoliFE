using MajoliFE.Data.Data;
using System;
using System.Collections.Generic;

namespace MajoliFE.Data.Interfaces
{
	public interface IInvoiceRepository : IRepository<Invoice>
	{
		List<Invoice> GetInvocesByCustomerId(int customerId);

		List<Invoice> GetInvocesFromRange(DateTime dateFrom, DateTime dateTo);
	}
}
