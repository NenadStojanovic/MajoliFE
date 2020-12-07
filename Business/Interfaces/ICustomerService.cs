using MajoliFE.Business.Dtos;
using MajoliFE.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MajoliFE.Business.Interfaces
{
	public interface ICustomerService
	{
		public IEnumerable<CustomerDto> GetAll();
		public void Create(CustomerDto model);

		public CustomerDto GetById(int id);

		public void Update(CustomerDto model);
	}
}
