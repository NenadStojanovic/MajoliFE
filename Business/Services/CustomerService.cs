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

		public void CreateCustomer(CustomerDto customer)
		{
			_customerRepository.Create(_mapper.Map<Customer>(customer));
			_customerRepository.SaveChanges();
		}

		public IEnumerable<CustomerDto> GetAllCustomers()
		{
			var result = _customerRepository.GetAll();
			var mappedResult = _mapper.Map<IEnumerable<CustomerDto>>(result);
			return mappedResult;
		}
	}
}
