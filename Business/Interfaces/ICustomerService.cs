using MajoliFE.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MajoliFE.Business.Interfaces
{
	public interface ICustomerService
	{
		public IEnumerable<Customer> GetAllCustomers();
		public void CreateCustomer(Customer customer);
	}
}
