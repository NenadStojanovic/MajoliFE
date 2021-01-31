using MajoliFE.Data.Data;
using System.Collections.Generic;

namespace MajoliFE.Data.Interfaces
{
	public interface IVendorInvoiceRepository : IRepository<VendorInvoice>
	{
		public List<VendorInvoice> GetInvocesByVendorId(int vendorId);
	}
}
