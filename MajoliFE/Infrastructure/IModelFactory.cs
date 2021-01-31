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

		public InvoicesViewModel FilterInvoicesVM(InvoicesFilterModel filterModel);

		public SettingsViewModel PrepareSettingsVM();
		public IndexViewModel PrepareIndexViewModel();

		public VendorsViewModel PrepareVendorsVM();
		public VendorInvoicesViewModel PrepareVendorInvoicesVM();
		public CreateOrUpdateVendorInvoicesViewModel PrepareCreateOrUpdateVendorInvoicesViewModel(int id);
	}
}
