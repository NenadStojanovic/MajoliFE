using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajoliFE.Data.Data
{
	[Table("Vendors")]
	public class Vendor : BaseEntity
	{
		public string Name { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string Zip { get; set; }
		public string PIB { get; set; }
		public string MB { get; set; }
		public string AccountNumber { get; set; }
		public virtual ICollection<VendorInvoice> VendorInvoices { get; set; }
	}
}
