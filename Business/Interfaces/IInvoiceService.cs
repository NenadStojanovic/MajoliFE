using MajoliFE.Business.Dtos;
using System.Collections.Generic;

namespace MajoliFE.Business.Interfaces
{
	public interface IInvoiceService
	{
		public IEnumerable<InvoiceDto> GetAll();
		public void Create(InvoiceDto model);

		public InvoiceDto GetById(int id);

		public void Update(InvoiceDto model);

		public void DeleteInvoice(int id);
		public void DownloadInvoice(int invoiceId);
	}
}
