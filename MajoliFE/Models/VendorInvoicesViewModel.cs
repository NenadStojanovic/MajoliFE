using MajoliFE.Business.Dtos;
using System.Collections.Generic;

namespace MajoliFE.Models
{
	public class VendorInvoicesViewModel : BaseViewModel
	{
		public IEnumerable<VendorInvoiceDto> VendorInvoices { get; set; }
		public VendorInvoiceStatistics VendorInvoiceStatistics { get; set; }
	}
}
