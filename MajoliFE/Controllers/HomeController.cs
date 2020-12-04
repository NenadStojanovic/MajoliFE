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

namespace MajoliFE.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IModelFactory _modelFactory;
		private readonly ICustomerService _customerService;

		public HomeController(ILogger<HomeController> logger, IModelFactory modelFactory, ICustomerService customerService)
		{
			_logger = logger;
			_modelFactory = modelFactory;
			_customerService = customerService;
		}

		public IActionResult Index()
		{
			//throw new Exception("Test");
		    //_customerService.CreateCustomer(new CustomerDto { Name = "Pera" });
			var model = _modelFactory.PrepareCustomersVM();
			return View();
		}

		[HttpGet]
		public IActionResult Customers()
		{
			var model = _modelFactory.PrepareCustomersVM();
			model.Customers = model.Customers.OrderByDescending(x => x.Id).ToList();
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
				model = _customerService.GetCustomerById(customerId);
			}
			return PartialView("_CreateOrUpdateCustomerDialog", model);
		}

		[HttpPost]
		public IActionResult CreateOrUpdateCustomerDialog(CustomerDto customer)
		{
			if (customer.Id == 0)
			{
				_customerService.CreateCustomer(customer);
			}
			else
			{
				_customerService.UpdateCustomer(customer);
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
	}
}
