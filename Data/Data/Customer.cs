using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MajoliFE.Data.Data
{
	[Table("Customers")]
	public class Customer : BaseEntity
	{
		public string Name { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string Zip { get; set; }
		public string PIB { get; set; }
		public string MB { get; set; }
		public string PartnerId { get; set; }
		public virtual ICollection<Invoice> Invoices { get; set; }
	}
}
