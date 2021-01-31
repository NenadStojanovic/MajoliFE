using System;

namespace MajoliFE.Business.Dtos
{
	public class VendorInvoiceDto : BaseDto
	{
		public float Total { get; set; }
		public float TotalPaid { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;
		public string Model { get; set; }
		public string ReferenceNumber { get; set; }
		public string Note { get; set; }
		public int VendorId { get; set; }
		public VendorDto Vendor { get; set; }
	}
}
