using MajoliFE.Business.Dtos;
using System.Collections.Generic;

namespace MajoliFE.Business.Interfaces
{
	public interface IVendorInvoiceService
	{
		public IEnumerable<VendorInvoiceDto> GetAll();
		public void Create(VendorInvoiceDto model);

		public VendorInvoiceDto GetById(int id);

		public void Update(VendorInvoiceDto model);
		void Delete(int id);
	}
}
