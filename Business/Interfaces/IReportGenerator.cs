using MajoliFE.Business.Dtos;
using System;

namespace MajoliFE.Business.Interfaces
{
	public interface IReportGenerator
	{
		public Report GenerateInvoicesReport(DateTime dateFrom, DateTime dateTo);
		public InvoiceReport GenerateInvoice(int invoiceId);

		public InvoiceStatistics GetInvoiceStatistics(DateTime? dateFrom = null, DateTime? dateTo = null);
		public VendorInvoiceStatistics GetVendorInvoiceStatistics(DateTime? dateFrom = null, DateTime? dateTo = null);
		public Report GenerateVendorInvoicesReport(DateTime dateFrom, DateTime dateTo);

	}
}
