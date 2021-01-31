namespace MajoliFE.Business.Dtos
{
	public class InvoiceStatistics
	{
		public int TotalNumOfInvoices { get; set; }
		public int TotalNumOfPaidInvoices { get; set; }
		public int TotalNumOfIssuedInvoices { get; set; }
		public float TotalBaseAmount { get; set; }
		public float TotalPDVAmount { get; set; }

		public float TotalAmount { get; set; }
		public float TotalPaidAmount { get; set; }
		public float TotalUnpaidAmount { get { return TotalAmount - TotalPaidAmount; } }

	}
}
