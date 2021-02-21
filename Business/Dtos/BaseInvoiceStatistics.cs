namespace MajoliFE.Business.Dtos
{
	public class BaseInvoiceStatistics
	{
		public int TotalNumOfInvoices { get; set; }
		public int TotalNumOfPaidInvoices { get; set; }
		public float TotalAmount { get; set; }
		public float TotalPaidAmount { get; set; }
		public float TotalUnpaidAmount { get { return TotalAmount - TotalPaidAmount; } }

	}
}
