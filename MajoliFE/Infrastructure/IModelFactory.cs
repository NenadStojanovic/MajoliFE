using MajoliFE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajoliFE.Infrastructure
{
	public interface IModelFactory
	{
		public CustomersViewModel PrepareCustomersVM();
		public InvoicesViewModel PrepareInvoicesVM();

		public CreateOrUpdateInvoiceViewModel PrepareCreateOrUpdateInvoiceVM(int customerId);
	}
}
