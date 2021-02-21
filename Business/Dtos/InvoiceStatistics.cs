namespace MajoliFE.Business.Dtos
{
	public class InvoiceStatistics : BaseInvoiceStatistics
	{
		public int TotalNumOfIssuedInvoices { get; set; }
		public float TotalBaseAmount { get; set; }
		public float TotalPDVAmount { get; set; }


	}
}
