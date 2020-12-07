using AutoMapper;
using MajoliFE.Business.Dtos;
using MajoliFE.Business.Interfaces;
using MajoliFE.Data.Data;
using MajoliFE.Data.Interfaces;
using System.Collections.Generic;

namespace MajoliFE.Business.Services
{
	class InvoiceService : IInvoiceService
	{
		private IInvoiceRepository _invoiceRepository;
		private readonly IMapper _mapper;
		public InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper)
		{
			_invoiceRepository = invoiceRepository;
			_mapper = mapper;
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
	}
}
