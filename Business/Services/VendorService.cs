using AutoMapper;
using MajoliFE.Business.Dtos;
using MajoliFE.Business.Interfaces;
using MajoliFE.Data.Data;
using MajoliFE.Data.Interfaces;
using System.Collections.Generic;

namespace MajoliFE.Business.Services
{
	class VendorService : IVendorService
	{
		private IVendorRepository _vendorRepository;
		private IVendorInvoiceRepository _vendorInvoiceRepository;
		private readonly IMapper _mapper;
		public VendorService(IVendorRepository vendorRepository, IMapper mapper, IVendorInvoiceRepository vendorInvoiceRepository)
		{
			_vendorRepository = vendorRepository;
			_mapper = mapper;
			_vendorInvoiceRepository = vendorInvoiceRepository;
		}

		public void Create(VendorDto vendor)
		{
			_vendorRepository.Create(_mapper.Map<Vendor>(vendor));
			_vendorRepository.SaveChanges();
		}

		public void Update(VendorDto vendor)
		{
			_vendorRepository.Update(vendor.Id, _mapper.Map<Vendor>(vendor));
			_vendorRepository.SaveChanges();
		}

		public IEnumerable<VendorDto> GetAll()
		{
			var result = _vendorRepository.GetAll();
			var mappedResult = _mapper.Map<IEnumerable<VendorDto>>(result);
			return mappedResult;
		}

		public VendorDto GetById(int vendorId)
		{
			var result = _vendorRepository.GetById(vendorId);
			var mappedResult = _mapper.Map<VendorDto>(result);
			return mappedResult;
		}
		/// <summary>
		/// Delete vendor by id
		/// </summary>
		/// <param name="vendorId"></param>
		/// <returns>True if vendor can be deleted, false if cannot because it is connected with invoice</returns>
		public bool Delete(int vendorId)
		{
			var invoices = _vendorInvoiceRepository.GetInvocesByVendorId(vendorId);
			if (invoices != null && invoices.Count > 0) //if vendor is connected with at least one invoice, return false
			{
				return false;
			}
			else
			{
				var vendor = _vendorRepository.GetById(vendorId);
				if (vendor != null)
				{
					_vendorRepository.Delete(vendor);
					_vendorRepository.SaveChanges();
					return true;
				}
				return true;

			}
		}
	}
}
