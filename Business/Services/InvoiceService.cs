using AutoMapper;
using MajoliFE.Business.Dtos;
using MajoliFE.Business.Interfaces;
using MajoliFE.Data.Data;
using MajoliFE.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MajoliFE.Business.Services
{
	class InvoiceService : IInvoiceService
	{
		private IInvoiceRepository _invoiceRepository;
		private IInvoiceItemRepository _invoiceItemRepository;
		private readonly IMapper _mapper;
		private readonly IReportGenerator _reportGenerator;
		public InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper, IInvoiceItemRepository invoiceItemRepository, IReportGenerator reportGenerator)
		{
			_invoiceRepository = invoiceRepository;
			_mapper = mapper;
			_invoiceItemRepository = invoiceItemRepository;
			_reportGenerator = reportGenerator;
		}

		public void Create(InvoiceDto model)
		{
			_invoiceRepository.Create(_mapper.Map<Invoice>(model));
			_invoiceRepository.SaveChanges();
		}

		public void Update(InvoiceDto model)
		{
			_invoiceRepository.Update(model.Id, _mapper.Map<Invoice>(model));
			_invoiceRepository.SaveChanges();
		}

		public IEnumerable<InvoiceDto> GetAll()
		{
			var result = _invoiceRepository.GetAll();
			var mappedResult = _mapper.Map<IEnumerable<InvoiceDto>>(result);
			return mappedResult;
		}

		public InvoiceDto GetById(int id)
		{
			var result = _invoiceRepository.GetById(id);
			var mappedResult = _mapper.Map<InvoiceDto>(result);
			return mappedResult;
		}

		public void DeleteInvoice(int invoiceId)
		{
			var invoiceItems = _invoiceItemRepository.GetByInvoiceId(invoiceId);
			if(invoiceItems!=null)
			{
				foreach (var item in invoiceItems)
				{
					_invoiceItemRepository.Delete(item);
				}
			}
			var invoice = _invoiceRepository.GetById(invoiceId);
			_invoiceRepository.Delete(invoice);
			_invoiceRepository.SaveChanges();
		}

		public InvoiceReport GenerateInvoice(int invoiceId)
		{
			try
			{
				var result = _reportGenerator.GenerateInvoice(invoiceId);
				return result;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			
		}

		public InvoiceStatistics GetInvoiceStatistics(DateTime? dateFrom = null, DateTime? dateTo = null)
		{
			if(dateFrom == null)
			{
				dateFrom = DateTime.MinValue;
			}
			if(dateTo == null)
			{
				dateTo = DateTime.MaxValue;
			}
			InvoiceStatistics model = new InvoiceStatistics();
			var invoces = _invoiceRepository.GetInvocesFromRange((DateTime)dateFrom, (DateTime)dateTo);
			if(invoces != null && invoces.Count>0)
			{
				model.TotalNumOfInvoices = invoces.Count;
				model.TotalNumOfIssuedInvoices = invoces.Where(x => x.IsIssued == true).ToList()?.Count??0;
				model.TotalNumOfPaidInvoices = invoces.Where(x => x.IsPaid == true).ToList()?.Count ?? 0;
				model.TotalAmount = invoces.Sum(x => x.Total);
				model.TotalPDVAmount = invoces.Sum(x => x.PDV);
				model.TotalBaseAmount = invoces.Sum(x => x.BaseTotal);
				model.TotalPaidAmount = invoces.Sum(x => x.TotalPaid);
			}
			return model;
		}
	}
}
