using AutoMapper;
using MajoliFE.Business.Dtos;
using MajoliFE.Business.Interfaces;
using MajoliFE.Data.Data;
using MajoliFE.Data.Interfaces;
using System.Collections.Generic;

namespace MajoliFE.Business.Services
{
	class InvoiceItemService : IInvoiceItemService
	{
		private IInvoiceItemRepository _invoiceItemRepository;
		private readonly IMapper _mapper;
		public InvoiceItemService(IInvoiceItemRepository invoiceItemRepository, IMapper mapper)
		{
			_invoiceItemRepository = invoiceItemRepository;
			_mapper = mapper;
		}

		public void Create(InvoiceItemDto model)
		{
			_invoiceItemRepository.Create(_mapper.Map<InvoiceItem>(model));
			_invoiceItemRepository.SaveChanges();
		}

		public void Update(InvoiceItemDto model)
		{
			_invoiceItemRepository.Update(model.Id, _mapper.Map<InvoiceItem>(model));
			_invoiceItemRepository.SaveChanges();
		}

		public IEnumerable<InvoiceItemDto> GetAll()
		{
			var result = _invoiceItemRepository.GetAll();
			var mappedResult = _mapper.Map<IEnumerable<InvoiceItemDto>>(result);
			return mappedResult;
		}

		public InvoiceItemDto GetById(int id)
		{
			var result = _invoiceItemRepository.GetById(id);
			var mappedResult = _mapper.Map<InvoiceItemDto>(result);
			return mappedResult;
		}

		public IEnumerable<InvoiceItemDto> GetByInvoiceId(int invoiceId)
		{
			var result = _invoiceItemRepository.GetByInvoiceId(invoiceId);
			var mappedResult = _mapper.Map<IEnumerable<InvoiceItemDto>>(result);
			return mappedResult;
		}

		public void DeleteInvoiceItem(int invoiceItemId)
		{
			if(invoiceItemId != 0)
			{
				var item = _invoiceItemRepository.GetById(invoiceItemId);
				if(item!=null)
				{
					_invoiceItemRepository.Delete(item);
					_invoiceItemRepository.SaveChanges();
				}
			}
		}

	}
}
