using MajoliFE.Business.Dtos;

namespace MajoliFE.Business.Interfaces
{
	public interface IReportGenerator
	{ 
		public InvoiceReport GenerateInvoice(int invoiceId);

	}
}
