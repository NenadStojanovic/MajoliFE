using MajoliFE.Business.Dtos;
using System.Collections.Generic;

namespace MajoliFE.Business.Interfaces
{
	public interface IInvoiceItemService
	{
		public IEnumerable<InvoiceItemDto> GetAll();
		public void Create(InvoiceItemDto model);

		public InvoiceItemDto GetById(int id);

		public void Update(InvoiceItemDto model);

		IEnumerable<InvoiceItemDto> GetByInvoiceId(int invoiceId);
		void DeleteInvoiceItem(int invoiceItemId);
	}
}
