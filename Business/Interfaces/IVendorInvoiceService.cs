using MajoliFE.Business.Dtos;
using System;
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
		public VendorInvoiceStatistics GetVendorInvoiceStatistics(DateTime? dateFrom = null, DateTime? dateTo = null);

		public Report GenerateInvoicesReport(string dateFrom, string dateTo);
	}
}
