using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MajoliFE.Models;
using MajoliFE.Business.Interfaces;
using MajoliFE.Business.Dtos;
using MajoliFE.Infrastructure;
using Microsoft.AspNetCore.Localization;
using System.Threading;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace MajoliFE.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IModelFactory _modelFactory;
		private readonly ICustomerService _customerService;
		private readonly IInvoiceService _invoiceService;
		private readonly IInvoiceItemService _invoiceItemService;
		private readonly ISettingsService _settingsService;
		private readonly IVendorService _vendorService;
		private readonly IVendorInvoiceService _vendorInvoiceService;
		public HomeController(ILogger<HomeController> logger, 
			IModelFactory modelFactory, 
			ICustomerService customerService, 
			IInvoiceService invoiceService, 
			IInvoiceItemService invoiceItemService,
			ISettingsService settingsService,
			IVendorService vendorService,
			IVendorInvoiceService vendorInvoiceService)
		{
			_logger = logger;
			_modelFactory = modelFactory;
			_customerService = customerService;
			_invoiceService = invoiceService;
			_invoiceItemService = invoiceItemService;
			_settingsService = settingsService;
			_vendorService = vendorService;
			_vendorInvoiceService = vendorInvoiceService;
		}

		public IActionResult Index()
		{
			//throw new Exception("Test");
			//_customerService.CreateCustomer(new CustomerDto { Name = "Pera" });
			//var model = _modelFactory.PrepareCustomersVM();
			var model = _modelFactory.PrepareIndexViewModel();
			return View(model);
		}

		[HttpGet]
		public IActionResult Customers()
		{
			var model = _modelFactory.PrepareCustomersVM();
			if(TempData["ShowMessage"]!=null)
			{
				model.ShowMessage = (bool)TempData["ShowMessage"];
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Vendors()
		{
			var model = _modelFactory.PrepareVendorsVM();
			if (TempData["ShowMessage"] != null)
			{
				model.ShowMessage = (bool)TempData["ShowMessage"];
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult VendorInvoices()
		{
			var model = _modelFactory.PrepareVendorInvoicesVM();
			if (TempData["ShowMessage"] != null)
			{
				model.ShowMessage = (bool)TempData["ShowMessage"];
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult CreateOrUpdateVendorInvoiceDialog(int vendorInvoiceId)
		{
			var model = new CreateOrUpdateVendorInvoicesViewModel();
			model = _modelFactory.PrepareCreateOrUpdateVendorInvoicesViewModel(vendorInvoiceId);
			return PartialView("_CreateOrUpdateVendorInvoiceDialog", model);
		}

		[HttpGet]
		public IActionResult CreateOrUpdateCustomerDialog(int customerId)
		{
			var model = new CustomerDto();
			if (customerId == 0)
			{
				return PartialView("_CreateOrUpdateCustomerDialog", model);
			}
			else
			{
				model = _customerService.GetById(customerId);
			}
			return PartialView("_CreateOrUpdateCustomerDialog", model);
		}

		[HttpGet]
		public IActionResult CreateOrUpdateVendorDialog(int vendorId)
		{
			var model = new VendorDto();
			if (vendorId == 0)
			{
				return PartialView("_CreateOrUpdateVendorDialog", model);
			}
			else
			{
				model = _vendorService.GetById(vendorId);
			}
			return PartialView("_CreateOrUpdateVendorDialog", model);
		}

		[HttpGet]
		public IActionResult CreateOrUpdateInvoiceItemDialog(int invoiceItemId, int index, bool isAdd, string name, string itemId, int quantity, float price)
		{
			var model = new InvoiceItemDto();
			model.Index = index;
			model.IsAdd = isAdd;
			if (invoiceItemId == 0)
			{
				if(!isAdd)
				{
					model.Name = name;
					model.ItemId = itemId;
					model.Quantity = quantity;
					model.Price = price;
				}
				return PartialView("_CreateOrUpdateInvoiceItemDialog", model);
			}
			else
			{
				model = _invoiceItemService.GetById(invoiceItemId);
				model.Index = index;
				model.IsAdd = isAdd;

			}
			return PartialView("_CreateOrUpdateInvoiceItemDialog", model);
		}

		

		[HttpPost]
		public IActionResult CreateOrUpdateCustomerDialog(CustomerDto customer)
		{
			if (customer.Id == 0)
			{
				_customerService.Create(customer);
			}
			else
			{
				_customerService.Update(customer);
			}
			TempData["ShowMessage"] = true;
			return RedirectToAction("Customers");
		}

		[HttpPost]
		public IActionResult CreateOrUpdateVendorInvoiceDialog(VendorInvoiceDto vendorInvoice)
		{
			if (vendorInvoice.Id == 0)
			{
				_vendorInvoiceService.Create(vendorInvoice);
			}
			else
			{
				_vendorInvoiceService.Update(vendorInvoice);
			}
			TempData["ShowMessage"] = true;
			return RedirectToAction("VendorInvoices");
		}

		[HttpPost]
		public IActionResult CreateOrUpdateVendorDialog(VendorDto vendor)
		{
			if (vendor.Id == 0)
			{
				_vendorService.Create(vendor);
			}
			else
			{
				_vendorService.Update(vendor);
			}
			TempData["ShowMessage"] = true;
			return RedirectToAction("Vendors");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpGet]
		public IActionResult Invoices(InvoicesFilterModel filterModel)
		{
			var model = _modelFactory.FilterInvoicesVM(filterModel);
			if (TempData["ShowMessage"] != null)
			{
				model.ShowMessage = (bool)TempData["ShowMessage"];
			}
			return View(model);
		}

		//[HttpPost]
		//public IActionResult Invoices(InvoicesFilterModel filterModel)
		//{
		//	var model = _modelFactory.FilterInvoicesVM(filterModel);
		//	return View(model);
		//}
		[HttpGet]
		public IActionResult CreateOrUpdateInvoice(int id)
		{
			var model = _modelFactory.PrepareCreateOrUpdateInvoiceVM(id);
			return View(model);
		}

		[HttpPost]
		public IActionResult CreateOrUpdateInvoice(InvoiceDto invoice)
		{
			if (invoice.Id == 0)
			{
				_invoiceService.Create(invoice);
			}
			else
			{
				_invoiceService.Update(invoice);
			}
			return Json("OK");
		}
		[HttpGet]
		public IActionResult GetCustomerById(int customerId)
		{
			if(customerId != 0)
			{
				var customer = _customerService.GetById(customerId);
				return Json(customer);
			}
			else
			{
				return Json(new CustomerDto());
			}
			
		}

		[HttpGet]
		public IActionResult DeleteCustomer(int customerId)
		{
			if (customerId != 0)
			{
				var result = _customerService.DeleteCustomer(customerId);
				return Json(result);
			}
			else
			{
				return Json(true);
			}

		}

		[HttpGet]
		public IActionResult DeleteVendor(int vendorId)
		{
			if (vendorId != 0)
			{
				var result = _vendorService.Delete(vendorId);
				return Json(result);
			}
			else
			{
				return Json(true);
			}

		}

		[HttpGet]
		public IActionResult DeleteVendorInvoice(int vendorInvoiceId)
		{
			if (vendorInvoiceId != 0)
			{
				_vendorInvoiceService.Delete(vendorInvoiceId);
				return Json("OK");
			}
			else
			{
				return Json("OK");
			}

		}

		[HttpGet]
		public IActionResult DeleteInvoice(int invoiceId)
		{
			if (invoiceId != 0)
			{
				_invoiceService.DeleteInvoice(invoiceId);
				
			}
			return Json(Ok());

		}

		[HttpGet]
		public IActionResult DeleteInvoiceItem(int invoiceItemId)
		{
			if (invoiceItemId != 0)
			{
				_invoiceItemService.DeleteInvoiceItem(invoiceItemId);

			}
			return Json(Ok());

		}

		[HttpPost]
		public IActionResult DownloadInvoice(int invoiceId)
		{
			if (invoiceId != 0)
			{
				try
				{
					var result = _invoiceService.GenerateInvoice(invoiceId);
					string handle = Guid.NewGuid().ToString();
					TempData[handle] = Convert.ToBase64String(result.InvoiceReportData);
					var fileName = "Racun_" + result.CustomerName + "_" + DateTime.Now.ToShortDateString()+ ".xlsx";
					var fileResult = new Models.FileResult() { FileGuid = handle, FileName = fileName };
					return Json(Ok(fileResult));
				}
				catch(Exception ex)
				{
					return Json(Error());
				}

				

			}
			return Json(Ok());

		}

		[HttpGet]
		public ActionResult Download(string fileGuid, string fileName)
		{
			if (TempData[fileGuid] != null)
			{
				var baseStringData = TempData[fileGuid] as string;
				byte[] data = Convert.FromBase64String(baseStringData);
				return File(data, "application/vnd.ms-excel", fileName);
			}
			else
			{
				// Problem - Log the error, generate a blank file,
				//           redirect to another controller action - whatever fits with your application
				return new EmptyResult();
			}
		}
		[HttpGet]
		public ActionResult Settings()
		{
			var model = _modelFactory.PrepareSettingsVM();
			if (TempData["ShowMessage"] != null)
			{
				model.ShowMessage = (bool)TempData["ShowMessage"];
			}
			return View(model);
		}

		[HttpPost]
		public ActionResult Settings(SettingsViewModel model)
		{
			if(model != null && model.Settings != null && model.Settings.Id != 0)
			{
				_settingsService.Update(model.Settings);
			}

			TempData["ShowMessage"] = true;
			return RedirectToAction("Settings");
		}
	}


}
