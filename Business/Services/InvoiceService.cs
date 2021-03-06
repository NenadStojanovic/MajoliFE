﻿using AutoMapper;
using MajoliFE.Business.Dtos;
using MajoliFE.Business.Interfaces;
using MajoliFE.Data.Data;
using MajoliFE.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
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

		public Report GenerateInvoicesReport(string dateFrom, string dateTo)
		{
			try
			{
				DateTime dateFromMapped = DateTime.Now.AddDays(-30);
				DateTime dateToMapped = DateTime.Now;
				if (dateFrom != null)
				{
				    dateFromMapped = DateTime.ParseExact(dateFrom, "dd.MM.yyyy", CultureInfo.CreateSpecificCulture("de-DE"));
				}
				if (dateTo != null)
				{
					dateToMapped = DateTime.ParseExact(dateTo, "dd.MM.yyyy", CultureInfo.CreateSpecificCulture("de-DE"));
				}
				var result = _reportGenerator.GenerateInvoicesReport(dateFromMapped, dateToMapped);
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public InvoiceStatistics GetInvoiceStatistics(DateTime? dateFrom = null, DateTime? dateTo = null)
		{
			var result = _reportGenerator.GetInvoiceStatistics(dateFrom, dateTo);
			return result;
		}
	}
}
