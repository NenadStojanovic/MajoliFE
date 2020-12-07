using MajoliFE.Business.Interfaces;
using MajoliFE.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace MajoliFE.Infrastructure
{
	public class ModelFactory : IModelFactory
	{
		private readonly ICustomerService _customersService;
		private readonly IInvoiceService _invoiceService;
		private readonly IInvoiceItemService _invoiceItemService;
		public ModelFactory(ICustomerService customersService, IInvoiceService invoiceService, IInvoiceItemService invoiceItemService)
		{
			_customersService = customersService;
			_invoiceService = invoiceService;
			_invoiceItemService = invoiceItemService;
		}

		public CreateOrUpdateInvoiceViewModel PrepareCreateOrUpdateInvoiceVM(int invoiceId)
		{
			var model = new CreateOrUpdateInvoiceViewModel();
			if (invoiceId != 0)
			{
				model.Invoice = _invoiceService.GetById(invoiceId);
				model.Invoice.InvoiceItems = _invoiceItemService.GetByInvoiceId(invoiceId).ToList();
			}
			else
			{
				model.Invoice = new Business.Dtos.InvoiceDto();
			}
			var customers = _customersService.GetAll();
			model.Customers = new SelectList(customers, "Id", "Name",model.Invoice.CustomerId);
			model.Settings = new Business.Dtos.SettingsDto();
			return model;
		}

		public CustomersViewModel PrepareCustomersVM()
		{
			var customers = _customersService.GetAll();
			var model = new CustomersViewModel() { Customers = customers };
			model.Customers = model.Customers.OrderByDescending(x => x.Id).ToList();
			return model;
		}

		public InvoicesViewModel PrepareInvoicesVM()
		{
			var invoices = _invoiceService.GetAll();
			var model = new InvoicesViewModel() { Invoices = invoices };
			model.Invoices = model.Invoices.OrderByDescending(x => x.Id).ToList();
			return model;
		}
	}
}
