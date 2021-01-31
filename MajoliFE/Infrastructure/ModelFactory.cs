using MajoliFE.Business.Interfaces;
using MajoliFE.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Globalization;
using System.Linq;

namespace MajoliFE.Infrastructure
{
	public class ModelFactory : IModelFactory
	{
		private readonly ICustomerService _customersService;
		private readonly IVendorService _vendorService;
		private readonly IInvoiceService _invoiceService;
		private readonly IInvoiceItemService _invoiceItemService;
		private readonly ISettingsService _settingsService;
		private readonly IVendorInvoiceService _vendorInvoiceService;
		public ModelFactory(ICustomerService customersService, IInvoiceService invoiceService, IInvoiceItemService invoiceItemService, ISettingsService settingsService, IVendorService vendorService, IVendorInvoiceService vendorInvoiceService)
		{
			_customersService = customersService;
			_invoiceService = invoiceService;
			_invoiceItemService = invoiceItemService;
			_settingsService = settingsService;
			_vendorService = vendorService;
			_vendorInvoiceService = vendorInvoiceService;
		}

		public CreateOrUpdateInvoiceViewModel PrepareCreateOrUpdateInvoiceVM(int invoiceId)
		{
			var model = new CreateOrUpdateInvoiceViewModel();
			if (invoiceId != 0)
			{
				model.Invoice = _invoiceService.GetById(invoiceId);
				//model.Invoice.InvoiceItems = _invoiceItemService.GetByInvoiceId(invoiceId).ToList();
			}
			else
			{
				model.Invoice = new Business.Dtos.InvoiceDto() { CurrencyDate = DateTime.Now, DateIssued = DateTime.Now, DateOfService = DateTime.Now};
			}
			var customers = _customersService.GetAll();
			model.Customers = new SelectList(customers, "Id", "Name",model.Invoice.CustomerId);
			var settings = _settingsService.GetActiveSettings();
			model.Settings = settings;
			return model;
		}

		public CustomersViewModel PrepareCustomersVM()
		{
			var customers = _customersService.GetAll();
			var model = new CustomersViewModel() { Customers = customers };
			model.Customers = model.Customers.OrderByDescending(x => x.Id).ToList();
			return model;
		}

		public VendorsViewModel PrepareVendorsVM()
		{
			var vendors = _vendorService.GetAll();
			var model = new VendorsViewModel() { Vendors = vendors};
			model.Vendors = model.Vendors.OrderByDescending(x => x.Id).ToList();
			return model;
		}

		public InvoicesViewModel PrepareInvoicesVM()
		{
			var invoices = _invoiceService.GetAll();
			var model = new InvoicesViewModel() { Invoices = invoices, FilterModel = new InvoicesFilterModel() };
			model.Invoices = model.Invoices.OrderByDescending(x => x.Id).ToList();
			var customers = _customersService.GetAll();
			model.FilterModel.Customers = customers.Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.Id.ToString()
			});
			return model;
		}

		public CreateOrUpdateVendorInvoicesViewModel PrepareCreateOrUpdateVendorInvoicesViewModel(int id)
		{
			var model = new CreateOrUpdateVendorInvoicesViewModel();
			if(id==0)
			{
				model.VendorInvoice = new Business.Dtos.VendorInvoiceDto();
			}
			else
			{
				model.VendorInvoice = _vendorInvoiceService.GetById(id);
			}
			var vendors = _vendorService.GetAll();
			model.Vendors = vendors.Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.Id.ToString()
			});
			return model;
		}
		public VendorInvoicesViewModel PrepareVendorInvoicesVM()
		{
			var vendoriInvoices = _vendorInvoiceService.GetAll();
			var model = new VendorInvoicesViewModel() { VendorInvoices = vendoriInvoices};
			model.VendorInvoices = model.VendorInvoices.OrderByDescending(x => x.Id).ToList();
			var customers = _customersService.GetAll();
			return model;
		}

		public InvoicesViewModel FilterInvoicesVM(InvoicesFilterModel filterModel)
		{
			var invoices = _invoiceService.GetAll();
			if(filterModel != null)
			{
				if (filterModel.DateFrom != null)
				{
					var dateFrom = DateTime.ParseExact(filterModel.DateFrom, "dd.MM.yyyy", CultureInfo.CreateSpecificCulture("de-DE"));
					invoices = invoices.Where(x => x.DateIssued >= dateFrom).ToList();
				}
				if (filterModel.DateTo != null)
				{
					var dateTo = DateTime.ParseExact(filterModel.DateTo, "dd.MM.yyyy", CultureInfo.CreateSpecificCulture("de-DE"));
					invoices = invoices.Where(x => x.DateIssued <= dateTo).ToList();
				}
			}
			var model = new InvoicesViewModel() { Invoices = invoices, FilterModel = new InvoicesFilterModel() };
			model.Invoices = model.Invoices.OrderByDescending(x => x.Id).ToList();
			var customers = _customersService.GetAll();
			model.FilterModel.Customers = customers.Select(x => new SelectListItem()
			{
				Text = x.Name,
				Value = x.Id.ToString()
			});
			model.InvoiceStatistics = _invoiceService.GetInvoiceStatistics();
			return model;
		}

		public SettingsViewModel PrepareSettingsVM()
		{
			var model = new SettingsViewModel();
			var result = _settingsService.GetActiveSettings();
			model.Settings = result;
			return model;
		}

		public IndexViewModel PrepareIndexViewModel()
		{
			IndexViewModel model = new IndexViewModel();
			model.InvoiceStatistics = _invoiceService.GetInvoiceStatistics();
			return model;
		}

	}
}
