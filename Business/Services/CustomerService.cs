using AutoMapper;
using MajoliFE.Business.Dtos;
using MajoliFE.Business.Interfaces;
using MajoliFE.Data.Data;
using MajoliFE.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MajoliFE.Business.Services
{
	class CustomerService : ICustomerService
	{
		private ICustomerRepository _customerRepository;
		private IInvoiceRepository _invoiceRepository;
		private readonly IMapper _mapper;
		public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IInvoiceRepository invoiceRepository)
		{
			_customerRepository = customerRepository;
			_mapper = mapper;
			_invoiceRepository = invoiceRepository;
		}

		public void Create(CustomerDto customer)
		{
			_customerRepository.Create(_mapper.Map<Customer>(customer));
			_customerRepository.SaveChanges();
		}

		public void Update(CustomerDto customer)
		{
			_customerRepository.Update(customer.Id, _mapper.Map<Customer>(customer));
			_customerRepository.SaveChanges();
		}

		public IEnumerable<CustomerDto> GetAll()
		{
			var result = _customerRepository.GetAll();
			var mappedResult = _mapper.Map<IEnumerable<CustomerDto>>(result);
			return mappedResult;
		}

		public CustomerDto GetById(int customerId)
		{
			var result = _customerRepository.GetById(customerId);
			var mappedResult = _mapper.Map<CustomerDto>(result);
			return mappedResult;
		}
		/// <summary>
		/// Delete customer by id
		/// </summary>
		/// <param name="customerId"></param>
		/// <returns>True if customer can be deleted, false if cannot because it is connected with invoice</returns>
		public bool DeleteCustomer(int customerId)
		{
			var invoices = _invoiceRepository.GetInvocesByCustomerId(customerId);
			if(invoices != null && invoices.Count>0) //if customer is connected with at least one invoice, return false
			{
				return false;
			}
			else
			{
				var customer = _customerRepository.GetById(customerId);
				if(customer!=null)
				{
					_customerRepository.Delete(customer);
					_customerRepository.SaveChanges();
					return true;
				}
				return true;
				
			}
		}
	}
}
