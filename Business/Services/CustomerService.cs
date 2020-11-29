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

		public CustomerService(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}

		public void CreateCustomer(Customer customer)
		{
			_customerRepository.Create(customer);
			_customerRepository.SaveChanges();
		}

		public IEnumerable<Customer> GetAllCustomers()
		{
			return _customerRepository.GetAll();
		}
	}
}
