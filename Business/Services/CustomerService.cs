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
		private readonly IMapper _mapper;
		public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
		{
			_customerRepository = customerRepository;
			_mapper = mapper;
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
	}
}
