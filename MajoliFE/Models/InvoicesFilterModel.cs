using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace MajoliFE.Models
{
	public class InvoicesFilterModel
	{
		public string DateFrom { get; set; }
		public string DateTo { get; set; }
		public int? CustomerId { get; set; }
		public IEnumerable<SelectListItem> Customers { get; set; }
	}
}
