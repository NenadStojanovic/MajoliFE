using MajoliFE.Business.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajoliFE.Models
{
	public class CreateOrUpdateInvoiceViewModel
	{
		public InvoiceDto Invoice { get; set; }
		public SelectList Customers { get; set; }

		public SettingsDto Settings { get; set; }
	}
}
