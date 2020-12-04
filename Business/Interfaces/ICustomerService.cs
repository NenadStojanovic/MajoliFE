using MajoliFE.Business.Dtos;
using MajoliFE.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MajoliFE.Business.Interfaces
{
	public interface ICustomerService
	{
		public IEnumerable<CustomerDto> GetAllCustomers();
		public void CreateCustomer(CustomerDto customer);

		public CustomerDto GetCustomerById(int customerId);

		public void UpdateCustomer(CustomerDto customer);
	}
}
