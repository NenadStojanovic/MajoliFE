using MajoliFE.Business.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MajoliFE.Models
{
	public class CreateOrUpdateVendorInvoicesViewModel : BaseViewModel
	{
		public VendorInvoiceDto VendorInvoice { get; set; }
		public IEnumerable<SelectListItem> Vendors { get; set; }

	}
}
