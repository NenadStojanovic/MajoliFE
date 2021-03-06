﻿using MajoliFE.Business.Dtos;
using System;
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
		public InvoiceReport GenerateInvoice(int invoiceId);
		public Report GenerateInvoicesReport(string dateFrom, string dateTo);
		public InvoiceStatistics GetInvoiceStatistics(DateTime? dateFrom = null, DateTime? dateTo = null);
	}
}
