using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MajoliFE.Data.Data
{
	[Table("Invoices")]
	public class Invoice : BaseEntity
	{
		public int InvoiceNumber { get; set; }
		public DateTime DateIssued { get; set; }
		public DateTime DateOfService { get; set; }
		public string Place { get; set; }
		public DateTime CurrencyDate { get; set; }
		public int CurrencyDateNumOfDays { get; set; }
		public int CustomerId { get; set; }
		public float BaseTotal { get; set; }
		public float Total { get; set; }
		public float TotalPaid { get; set; }
		public int PDV { get; set; }
		public string Note { get; set; }
		public bool IsPaid { get; set; }
		public bool IsIssued { get; set; }

		public string CustomerName { get; set; }
		public string PartnerId { get; set; }
		public string CustomerAddress { get; set; }
		public string CustomerPIB { get; set; }
		public string CustomerMB { get; set; }

		public virtual Customer Customer { get; set; }
		public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
	}


}
