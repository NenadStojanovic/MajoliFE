using AutoMapper;
using MajoliFE.Business.Dtos;
using MajoliFE.Business.Interfaces;
using MajoliFE.Data.Data;
using MajoliFE.Data.Interfaces;
using System.Collections.Generic;

namespace MajoliFE.Business.Services
{
	class VendorinvoiceService : IVendorInvoiceService
	{
		private IVendorRepository _vendorRepository;
		private IVendorInvoiceRepository _vendorInvoiceRepository;
		private readonly IMapper _mapper;
		public VendorinvoiceService(IVendorRepository vendorRepository, IMapper mapper, IVendorInvoiceRepository vendorInvoiceRepository)
		{
			_vendorRepository = vendorRepository;
			_mapper = mapper;
			_vendorInvoiceRepository = vendorInvoiceRepository;
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

		
	}
}
