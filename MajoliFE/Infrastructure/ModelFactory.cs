using MajoliFE.Business.Interfaces;
using MajoliFE.Models;

namespace MajoliFE.Infrastructure
{
	public class ModelFactory : IModelFactory
	{
		private readonly ICustomerService _customersService;
		public ModelFactory(ICustomerService customersService)
		{
			_customersService = customersService;
		}
		public CustomersViewModel PrepareCustomersVM()
		{
			var customers = _customersService.GetAllCustomers();
			var model = new CustomersViewModel() { Customers = customers };
			return model;
		}
	}
}
