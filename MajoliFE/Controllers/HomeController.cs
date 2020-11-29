using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MajoliFE.Models;
using MajoliFE.Business.Interfaces;

namespace MajoliFE.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ICustomerService _customerService;

		public HomeController(ILogger<HomeController> logger, ICustomerService customerService)
		{
			_logger = logger;
			_customerService = customerService;
		}

		public IActionResult Index()
		{
			//throw new Exception("Test");
			//_customerService.CreateCustomer(new Data.Data.Customer() { Name = "Pera" });
			var all = _customerService.GetAllCustomers();
			return View();
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
