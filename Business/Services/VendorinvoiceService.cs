using AutoMapper;
using MajoliFE.Business.Dtos;
using MajoliFE.Business.Interfaces;
using MajoliFE.Data.Data;
using MajoliFE.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MajoliFE.Business.Services
{
	class VendorInvoiceService : IVendorInvoiceService
	{
		private IVendorRepository _vendorRepository;
		private IVendorInvoiceRepository _vendorInvoiceRepository;
		private readonly IMapper _mapper;
		private readonly IReportGenerator _reportGenerator;
		public VendorInvoiceService(IVendorRepository vendorRepository, IMapper mapper, IVendorInvoiceRepository vendorInvoiceRepository, IReportGenerator reportGenerator)
		{
			_vendorRepository = vendorRepository;
			_mapper = mapper;
			_vendorInvoiceRepository = vendorInvoiceRepository;
			_reportGenerator = reportGenerator;
		}

		public void Create(VendorInvoiceDto vendor)
		{
			_vendorInvoiceRepository.Create(_mapper.Map<VendorInvoice>(vendor));
			_vendorInvoiceRepository.SaveChanges();
		}

		public void Update(VendorInvoiceDto vendor)
		{
			_vendorInvoiceRepository.Update(vendor.Id, _mapper.Map<VendorInvoice>(vendor));
			_vendorInvoiceRepository.SaveChanges();
		}

		public IEnumerable<VendorInvoiceDto> GetAll()
		{
			var result = _vendorInvoiceRepository.GetAll();
			var mappedResult = _mapper.Map<IEnumerable<VendorInvoiceDto>>(result);
			return mappedResult;
		}

		public VendorInvoiceDto GetById(int vendorId)
		{
			var result = _vendorInvoiceRepository.GetById(vendorId);
			var mappedResult = _mapper.Map<VendorInvoiceDto>(result);
			return mappedResult;
		}
		public void Delete(int id)
		{
			if (id != 0)
			{
				var invoice = _vendorInvoiceRepository.GetById(id);
				if (invoice != null)
				{
					_vendorInvoiceRepository.Delete(invoice);
					_vendorInvoiceRepository.SaveChanges();
				}
			}
		}

		public VendorInvoiceStatistics GetVendorInvoiceStatistics(DateTime? dateFrom = null, DateTime? dateTo = null)
		{
			var result = _reportGenerator.GetVendorInvoiceStatistics(dateFrom, dateTo);
			return result;
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
				var result = _reportGenerator.GenerateVendorInvoicesReport(dateFromMapped, dateToMapped);
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


	}
}
