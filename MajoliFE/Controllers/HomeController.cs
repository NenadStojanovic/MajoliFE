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
	//[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IModelFactory _modelFactory;
		private readonly ICustomerService _customerService;
		private readonly IInvoiceService _invoiceService;
		private readonly IInvoiceItemService _invoiceItemService;

		public HomeController(ILogger<HomeController> logger, IModelFactory modelFactory, ICustomerService customerService, IInvoiceService invoiceService, IInvoiceItemService invoiceItemService)
		{
			_logger = logger;
			_modelFactory = modelFactory;
			_customerService = customerService;
			_invoiceService = invoiceService;
			_invoiceItemService = invoiceItemService;
		}

		public IActionResult Index()
		{
			//throw new Exception("Test");
		    //_customerService.CreateCustomer(new CustomerDto { Name = "Pera" });
			//var model = _modelFactory.PrepareCustomersVM();
			return View();
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
		public IActionResult CreateOrUpdateCustomerDialog(int customerId)
		{
			var model = new CustomerDto();
			if(customerId == 0)
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
		public IActionResult Invoices()
		{
			var model = _modelFactory.PrepareInvoicesVM();
			if (TempData["ShowMessage"] != null)
			{
				model.ShowMessage = (bool)TempData["ShowMessage"];
			}
			return View(model);
		}
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

		[HttpGet]
		public IActionResult DownloadInvoice(int invoiceId)
		{
			if (invoiceId != 0)
			{
				_invoiceService.DownloadInvoice(invoiceId);

			}
			return Json(Ok());

		}
	}
}
