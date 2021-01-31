using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajoliFE.Data.Data
{
	[Table("VendorInvoices")]
	public class VendorInvoice : BaseEntity
	{
		public float Total { get; set; }
		public float TotalPaid { get; set; }
		public DateTime Date { get; set; }
		public string Model { get; set; }
		public string ReferenceNumber { get; set; }
		public string Note { get; set; }
		public int VendorId { get; set; }
		public virtual Vendor Vendor{ get; set; }
	}
}
